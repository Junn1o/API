using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;

namespace API.Repositories
{
    public class SQLRoomRepository : IRoomRepository
    {
        private readonly AppDbContext _appDbContext;
        public SQLRoomRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<RoomDTO> GetAllRoom()
        {
            var roomlist = _appDbContext.Room.Select(room => new RoomDTO()
            {
                Id = room.Id,
                title = room.title,
                authorname = room.author.firstname + " " + room.author.lastname,
                address = room.address,
                description = room.description,
                price = room.price,
                isApprove = room.isApprove,
                isHire = room.isHire,
                actualFile = room.actualFile,
                area = room.area,
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
                authorname = room.author.firstname + " " + room.author.lastname,
                address = room.address,
                description = room.description,
                price = room.price,
                isApprove = room.isApprove,
                isHire = room.isHire,
                actualFile = room.actualFile,
                area = room.area,
                categorylist = room.room_category.Select(rc => rc.category.name).ToList()
            }).FirstOrDefault();
            return getRoombyDTO;
        }
        public List<RoomDTO> GetAllRoomisHire()
        {
            var roomlist = _appDbContext.Room.Where(room => room.isHire).Select(room => new RoomDTO()
            {
                Id = room.Id,
                title = room.title,
                authorname = room.author.firstname + " " + room.author.lastname,
                address = room.address,
                description = room.description,
                price = room.price,
                isApprove = room.isApprove,
                isHire = room.isHire,
                actualFile = room.actualFile,
                area = room.area,
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
                authorname = room.author.firstname + " " + room.author.lastname,
                address = room.address,
                description = room.description,
                price = room.price,
                isApprove = room.isApprove,
                isHire = room.isHire,
                actualFile = room.actualFile,
                area = room.area,
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
                authorname = room.author.firstname + " " + room.author.lastname,
                address = room.address,
                description = room.description,
                price = room.price,
                isApprove = room.isApprove,
                isHire = room.isHire,
                actualFile = room.actualFile,
                area = room.area,
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
                authorname = room.author.firstname + " " + room.author.lastname,
                address = room.address,
                description = room.description,
                price = room.price,
                isApprove = room.isApprove,
                isHire = room.isHire,
                actualFile = room.actualFile,
                area = room.area,
                categorylist = room.room_category.Select(rc => rc.category.name).ToList()
            }).ToList();
            return roomlist;
        }
        public AddRoomRequestDTO AddRoom(AddRoomRequestDTO addRoom)
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
                datecreatedroom = DateTime.Now,
            };
            _appDbContext.Room.Add(roomDomain);
            _appDbContext.SaveChanges();
            var author = _appDbContext.Author.FirstOrDefault(author => author.Id == addRoom.authorId);
            if (addRoom.FileUri != null && author!=null)
            {
                addRoom.actualFile = UploadImage(addRoom.FileUri, author.Id, author.datecreated.ToString("yyyy"), roomDomain.Id);
                roomDomain.actualFile = addRoom.actualFile;
                _appDbContext.SaveChanges();
            }
            foreach (var id in addRoom.categoryids)
            {
                var room_category = new Room_Category()
                {
                    roomId = roomDomain.Id,
                    categoryId = id,
                };
                _appDbContext.Room_Category.Add(room_category);
                _appDbContext.SaveChanges();
            }
            return addRoom;
        }
        public AddRoomRequestDTO UpdateRoom(int id, AddRoomRequestDTO updateRoom)
        {
            var roomDomain = _appDbContext.Room.FirstOrDefault(r => r.Id == id);
            if (roomDomain != null)
            {
                var authorDomain = _appDbContext.Author.FirstOrDefault(ad => ad.Id == roomDomain.authorId);
                if (authorDomain != null && updateRoom.FileUri != null)
                {
                    if (roomDomain.actualFile == null || AddNewImagesToPath(roomDomain.actualFile, updateRoom.FileUri) == null)
                    {
                        updateRoom.actualFile = UploadImage(updateRoom.FileUri, authorDomain.Id, authorDomain.datecreated.ToString("yyyy"), roomDomain.Id);
                        roomDomain.actualFile = updateRoom.actualFile;
                    }
                    else
                    {
                        updateRoom.actualFile = AddNewImagesToPath(roomDomain.actualFile, updateRoom.FileUri);
                        roomDomain.actualFile = updateRoom.actualFile;
                    }
                }
                roomDomain.title = updateRoom.title;
                roomDomain.price = updateRoom.price;
                roomDomain.address = updateRoom.address;
                roomDomain.description = updateRoom.description;
                roomDomain.authorId = updateRoom.authorId;
                roomDomain.isApprove = updateRoom.isApprove;
                roomDomain.isHire = updateRoom.isHire;
                _appDbContext.SaveChanges();
            }
            var categoryroomDomain = _appDbContext.Room_Category.Where(a => a.roomId == id).ToList();
            if (categoryroomDomain != null)
            {
                _appDbContext.Room_Category.RemoveRange(categoryroomDomain);
                _appDbContext.SaveChanges();
            }
            foreach (var categoryid in updateRoom.categoryids)
            {
                var room_category = new Room_Category()
                {
                    roomId = id,
                    categoryId = categoryid,
                };
                _appDbContext.Room_Category.Add(room_category);
                _appDbContext.SaveChanges();
            }
            return updateRoom;
        }
        public Room? DeleteRoomwithId(int id)
        {
            var roomDomain = _appDbContext.Room.FirstOrDefault(r => r.Id == id);
            var roomCategory = _appDbContext.Room_Category.Where(n => n.roomId == id);
            if (roomDomain != null)
            {
                if (roomDomain.actualFile != null)
                {
                    DeleteRoomImages(roomDomain.actualFile);
                }
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
        public string UploadImage(IFormFile[] file, int id, string adatecreated, int roomid)
        {
            int counter = 1;
            string picture = "";
            foreach (var image in file)
            {
                string count = $"{counter++}";
                var fileEx = Path.GetExtension(image.FileName);
                var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "author", id + "-" + adatecreated, "uploads", roomid.ToString());
                Directory.CreateDirectory(uploadFolderPath);
                var filePath = Path.Combine(uploadFolderPath, roomid + "-" + "image_" + count + fileEx);
                using (FileStream ms = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(ms);
                }
                string relativePath = Path.Combine("images", "author", id + "-" + adatecreated, "uploads", roomid.ToString(), roomid + "-" + "image_" + count);
                picture += relativePath + ";";
            }
            return picture;
        }
        public string AddNewImagesToPath(string imagePath, IFormFile[] newFiles)
        {
            string picture = imagePath;
            string[] existingImagePaths = imagePath.Split(';');
            string[] parts = existingImagePaths[0].Split('\\');
            string idAndDate = parts[2];
            string roomid = parts[4];
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "author", idAndDate, "uploads", roomid);
            if (!File.Exists(existingImagePaths[0]))
            {
                return null;
            }
            else
            {
                int startingCount = existingImagePaths.Length;

                foreach (var image in newFiles)
                {
                    string fileName = roomid + "-image_" + (startingCount++) + Path.GetExtension(image.FileName);

                    var filePath = Path.Combine(folderPath, fileName);
                    using (FileStream ms = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(ms);
                    }
                    string relativePath = Path.Combine("images", "author", idAndDate, "uploads", fileName);
                    picture += relativePath + ";";
                }
                return picture;
            }
        }
        public bool DeleteRoomImages(string imagePath)
        {
            string[] imagePaths = imagePath.Split(';');
            string folderPath = Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePaths[0]));
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
