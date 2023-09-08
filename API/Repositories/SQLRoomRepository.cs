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
                authorname = room.author.firstname+" "+room.author.lastname,
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
                title = room.title,
                authorname = room.author.firstname + " " + room.author.lastname,
                address = room.address,
                description = room.description,
                price = room.price,
                isApprove = room.isApprove,
                isHire = room.isHire,
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
            };
            _appDbContext.Room.Add(roomDomain);
            _appDbContext.SaveChanges();
            var author = _appDbContext.Author.FirstOrDefault(author => author.Id == roomDomain.Id);
            string path = "";
            if (author == null)
            {
                throw new FileNotFoundException("asgasgasg");
            }
            if (addRoom.FileUri != null)
            {
                path = UploadImage1(addRoom.FileUri, author.firstname, addRoom.title);
                addRoom.actualFile = path;
                roomDomain.actualFile = addRoom.actualFile;
                _appDbContext.Room.Add(roomDomain);
            }
            _appDbContext.SaveChanges();
            foreach (var id in addRoom.categoryids)
            {
                var room_category = new Room_Category()
                {
                    roomId = roomDomain.Id,
                    categoryId = id,
                };
                //_appDbContext.Room_Category.Add(room_category);
                //_appDbContext.SaveChanges();
            }
            return addRoom;
        }
        public AddRoomRequestDTO UpdateRoom(int id, AddRoomRequestDTO updateRoom)
        {
            var roomDomain = _appDbContext.Room.FirstOrDefault(r => r.Id == id);
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
            var categoryDomain = _appDbContext.Room_Category.Where(a => a.roomId == id).ToList();
            if (categoryDomain != null)
            {
                _appDbContext.Room_Category.RemoveRange(categoryDomain);
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
        public List<string> UploadImage(List<IFormFile> file, string author, string roomtitle)
        {
            int counter = 1;
            List<string> imagePaths = new List<string>();
            foreach (var image in file)
            {
                string count = $"{counter++}";
                var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user", author, "uploads", roomtitle, roomtitle + "-" + "image_" + count);
                Directory.CreateDirectory(uploadFolderPath);
                var filePath = Path.Combine(uploadFolderPath, roomtitle);
                using (FileStream ms = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(ms);
                }
                string relativePath = Path.Combine("images", "user", author, "uploads", roomtitle, roomtitle + "-" + "image_" + count);
                imagePaths.Add(relativePath);
            }
            return imagePaths;
        }
        public string UploadImage1(IFormFile[] file, string author, string roomtitle)
        {
            //var fileName = Path.GetFileName(image.FileName);
            int counter = 1;
            string picture = "";
            foreach (var image in file)
            {
                string count = $"{counter++}";
                var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user", author, "uploads", roomtitle, roomtitle + "-" + "image_" + count);
                Directory.CreateDirectory(uploadFolderPath);
                var filePath = Path.Combine(uploadFolderPath, roomtitle);
                using (FileStream ms = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(ms);
                }
                string relativePath = Path.Combine("images", "user", author, "uploads", roomtitle, roomtitle + "-" + "image_" + count);
                picture += relativePath + ";";
            }
            //var path = Path.Combine("images", "room", username + "-" + fileName, fileName);
            return picture;
        }
        //public string UploadImage2(IFormFile file, string author, string roomtitle)
        //{
        //    int counter = 1;
        //    List<string> imagePaths = new List<string>();
        //    foreach (var image in file)
        //    {
        //        string count = $"{counter++}";
        //        var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user", author, "uploads", roomtitle, roomtitle + "-" + "image_" + count);
        //        Directory.CreateDirectory(uploadFolderPath);
        //        var filePath = Path.Combine(uploadFolderPath, roomtitle);
        //        using (FileStream ms = new FileStream(filePath, FileMode.Create))
        //        {
        //            image.CopyTo(ms);
        //        }
        //        string relativePath = Path.Combine("images", "user", author, "uploads", roomtitle, roomtitle + "-" + "image_" + count);
        //        imagePaths.Add(relativePath);
        //    }
        //    return imagePaths;
        //}
        //public string UpdateImage(IFormFile newImage, string oldRelativePath, string username)
        //{
        //    if (oldRelativePath != null)
        //    {
        //        var oldFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldRelativePath);
        //        if (!File.Exists(oldFullPath))
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            var oldDirectory = Path.GetDirectoryName(oldFullPath);
        //            var newRelativePath = UploadImage(newImage, username);
        //            File.Delete(oldFullPath);
        //            return newRelativePath;
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        public bool DeleteImage(string oldRelativePath)
        {
            //string fileName = Path.GetFileName(oldRelativePath);
            string parentDirectoryName = Path.GetFileName(Path.GetDirectoryName(oldRelativePath));
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user", parentDirectoryName);
            //!File.Exists(filePath)
            if (!Directory.Exists(folderPath))
            {
                return false;
            }
            else
            {
                //File.Delete(filePath);
                Directory.Delete(folderPath, true);
                return true;
            }
        }
    }
}
