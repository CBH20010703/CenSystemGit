using System;
using System.Collections.Generic;
using System.Linq;

namespace CenBolgsSystem.Models
{

    public class BlogsUser : IBlogsOperation<User>
    {
        private int num { get; set; }
        public User ConditionQuery(User id)
        {
            throw new NotImplementedException();
        }

        public bool InsertData(User data)
        {
            try
            {
                db_CenSystemEntities db = new db_CenSystemEntities();
                db.User.Add(InitUser(data));
                return db.SaveChanges() <= 0 ? false : true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        // 初始化 用户
        private User InitUser(User data)
        {
            Random rd = new Random();
            int imgNumber = rd.Next(0, 5);
            string[] ImgUrl = { "/images/a1.png", "/images/a2.png", "/images/a3.png", "/images/a4.png", "/images/t2.png" };
            // 系统分配随机头像 images
            data.user_CreatDateTime = DateTime.Now;
            data.user_Type = 3;
            data.user_Status = false;
            data.user_ImgUrl = ImgUrl[imgNumber];
            return data;
        }
        public bool RemoveData(User data)
        {
            if (data.user_Status == true) return false;
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {
                List<BlogsLeave> list = db.BlogsLeave.Where(c => c.leave_userId == data.user_Id).ToList();
                foreach (var item in list)
                {
                    db.BlogsLeave.Remove(item);
                    db.SaveChanges();
                };
                db.User.Remove(db.User.FirstOrDefault(c => c.user_Id == data.user_Id));
                return db.SaveChanges() <= 0 ? false : true;
            }
        }

        public bool UpDataStatus(User Userstatus)
        {
            try
            {
                using (db_CenSystemEntities db = new db_CenSystemEntities())
                {
                    db.User.First(c => c.user_Id == Userstatus.user_Id).user_Status = Userstatus.user_Status;
                    return db.SaveChanges() <= 0 ? false : true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public int SelectCount()
        {
            return num;
        }

        public IQueryable SelectData(int page, int limit, string title, int? type = null)
        {
            db_CenSystemEntities db = new db_CenSystemEntities();
            if (string.IsNullOrEmpty(title))
            {

                num = db.User.Count();
                return db.User.OrderByDescending(c => c.user_Id).Skip((page - 1) * limit).Take(limit).Select(c => new
                {
                    c.user_Id,
                    c.user_Name,
                    c.user_Email,
                    c.user_PassWord,
                    c.user_Status,
                    c.UserType.userType_Name,
                    c.user_ImgUrl,
                    c.user_CreatDateTime
                });


            }
            num = db.User.Where(c => c.user_Name.Contains(title)).Count();
            return db.User.OrderByDescending(c => c.user_Id).Where(c => c.user_Name.Contains(title)).Skip((page - 1) * limit).Take(limit).Select(c => new
            {
                c.user_Id,
                c.user_Name,
                c.user_Email,
                c.user_PassWord,
                c.user_Status,
                c.UserType.userType_Name,
                c.user_ImgUrl,
                c.user_CreatDateTime
            });

        }

        public bool UpDateData(User data)
        {
            throw new NotImplementedException();
        }
    }
}