using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using System.IO;
namespace API.Repositories
{
    public class SQLRoomRepository : IRoomRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly string _uploadsPath;
        private readonly IImageRepository _imageRepository;
        public SQLRoomRepository(AppDbContext appDbContext, IImageRepository imageRepository)
        {
            _appDbContext = appDbContext;
            _imageRepository = imageRepository;
        }

        public List<RoomDTO> GetAllRoom()
        {
            var roomlist = _appDbContext.Room.Select(room => new RoomDTO()
            {
                Id = room.Id,
                title = room.title,
                authorname = room.author.fullname,
                address = room.address,
                description = room.description,
                price = room.price,
                isApprove = room.isApprove,
                isHire = room.isHire,
                categorylist = room.room_category.Select(rc => rc.category.name).ToList()
            }).ToList();
            return roomlist;
        }
        public RoomwithIdDTO GetRoomwithId(int id)
        {
            var getRoombyDomain = _appDbContext.Room.Where(rd => rd.Id == id);
            var getRoombyDTO = getRoombyDomain.Select(room => new RoomwithIdDTO()
            {
                title= room.title,
                authorname = room.author.fullname,
                address = room.address,
                description = room.description,
                price = room.price,
                isApprove = room.isApprove,
                isHire = room.isHire,
                categorylist = room.room_category.Select(rc=> rc.category.name).ToList()
            }).FirstOrDefault();
            return getRoombyDTO;
        }
        public List<RoomDTO> GetAllRoomisHire()
        {
            var roomlist = _appDbContext.Room.Where(room => room.isHire).Select(room => new RoomDTO()
            {
                Id = room.Id,
                title = room.title,
                authorname = room.author.fullname,
                address = room.address,
                description = room.description,
                price = room.price,
                isApprove = room.isApprove,
                isHire = room.isHire,
                categorylist = room.room_category.Select(rc => rc.category.name).ToList()
            }).ToList();
            return roomlist;
        }
        public List<RoomDTO> GetAllRoomnotHire()
        {
            var roomlist = _appDbContext.Room.Where(room => !room.isHire).Where(room => room.isApprove).Select(room => new RoomDTO()
            {
                Id = room.Id,
                title = room.title,
                authorname = room.author.fullname,
                address = room.address,
                description = room.description,
                price = room.price,
                isApprove = room.isApprove,
                isHire = room.isHire,
                categorylist = room.room_category.Select(rc => rc.category.name).ToList()
            }).ToList();
            return roomlist;
        }

        public List<RoomDTO> GetAllRoomnotApprove()
        {
            var roomlist = _appDbContext.Room.Where(room => !room.isApprove).Select(room => new RoomDTO()
            {
                Id = room.Id,
                title = room.title,
                authorname = room.author.fullname,
                address = room.address,
                description = room.description,
                price = room.price,
                isApprove = room.isApprove,
                isHire = room.isHire,
                categorylist = room.room_category.Select(rc => rc.category.name).ToList()
            }).ToList();
            return roomlist;
        }

        public List<RoomDTO> GetAllRoomisApprove()
        {
            var roomlist = _appDbContext.Room.Where(room => room.isApprove).Select(room => new RoomDTO()
            {
                Id = room.Id,
                title = room.title,
                authorname = room.author.fullname,
                address = room.address,
                description = room.description,
                price = room.price,
                isApprove = room.isApprove,
                isHire = room.isHire,
                categorylist = room.room_category.Select(rc => rc.category.name).ToList()
            }).ToList();
            return roomlist;
        }
        public AddRoomRequestDTO AddRoom(AddRoomRequestDTO addRoom, IFormFile imageFile)
        {
            var roomDomain = new Room
            {
                title = addRoom.title,
                price = addRoom.price,
                address = addRoom.address,
                description = addRoom.description,
                authorId = addRoom.authorId,
                isApprove = addRoom.isApprove,
                isHire = addRoom.isHire,
                area = addRoom.area,
            };
            _appDbContext.Room.Add(roomDomain);
            _appDbContext.SaveChanges();
            foreach(var id in addRoom.categoryids)
            {
                var room_category = new Room_Category()
                {
                    roomId = roomDomain.Id,
                    categoryId = id,
                };
                _appDbContext.Room_Category.Add(room_category);
                _appDbContext.SaveChanges();
            }
            foreach(var id in addRoom.roomId)
            {
                var room_photo = new Photo()
                {
                    roomId = roomDomain.Id,
                    Id = id,
                    imagepath=_imageRepository.SaveImage(imageFile)
                };
                
            }
            return addRoom;
        }
        public AddRoomRequestDTO UpdateRoom(int id, AddRoomRequestDTO updateRoom)
        {
            var roomDomain = _appDbContext.Room.FirstOrDefault(r=>r.Id == id);
            if (roomDomain != null)
            {
                roomDomain.title = updateRoom.title;
                roomDomain.price = updateRoom.price;
                roomDomain.address = updateRoom.address;
                roomDomain.description = updateRoom.description;
                roomDomain.authorId = updateRoom.authorId;
                roomDomain.isApprove = updateRoom.isApprove;
                roomDomain.isHire = updateRoom.isHire;
                _appDbContext.SaveChanges();
            }
            var categoryDomain = _appDbContext.Room_Category.Where(a=>a.roomId == id).ToList();
            if (categoryDomain!=null)
            {
                _appDbContext.Room_Category.RemoveRange(categoryDomain);
                _appDbContext.SaveChanges();
            }
            foreach(var categoryid in updateRoom.categoryids)
            {
                var room_category = new Room_Category()
                {
                    roomId=id,
                    categoryId=categoryid,
                };
                _appDbContext.Room_Category.Add(room_category);
                _appDbContext.SaveChanges();
            }
            return updateRoom;
        }
        public Room? DeleteRoomwithId(int id)
        {
            var roomDomain = _appDbContext.Room.FirstOrDefault(r=>r.Id == id);
            var roomCategory = _appDbContext.Room_Category.Where(n => n.roomId == id);
            if (roomDomain!=null)
            {
                if (roomCategory.Any())
                {
                    _appDbContext.Room_Category.RemoveRange(roomCategory);
                    _appDbContext.SaveChanges();
                }
                _appDbContext.Room.Remove(roomDomain);
                _appDbContext.SaveChanges();
            }
            return roomDomain;
        }
    }
}
