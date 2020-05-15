using feedback.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace feedback.Management
{
    public class UserMgt
    {
        FeedBackContext _ctx = null;
        public async Task<object> userCreate(User model)
        {
            object result = null; string message = string.Empty; bool resstate = false;
            using (_ctx = new FeedBackContext())
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {

                        if (model.UserId > 0)
                        {
                            var objUser = await _ctx.User.FirstOrDefaultAsync(x => x.UserId == model.UserId);
                            objUser.UserName = model.UserName;
                            objUser.UserType = model.UserType;
                            objUser.Password = model.Password;

                        }
                        else
                        {
                            var objUser = new User();
                            objUser.UserName = model.UserName;
                            objUser.UserType = model.UserType;
                            objUser.Password = model.Password;



                            if (objUser != null)
                            {
                                await _ctx.User.AddAsync(objUser);
                            }
                        }

                        await _ctx.SaveChangesAsync();

                        _ctxTransaction.Commit();
                        message = "Saved Successfully";
                        resstate = true;
                    }
                    catch (Exception ex)
                    {
                        _ctxTransaction.Rollback();
                        // Logs.WriteBug(ex);
                        message = "Failed to save.";
                        resstate = false;
                    }
                }
            }
            return result = new
            {
                message,
                resstate
            };
        }
        public async Task<object> userAuthentication(User model)
        {
            object result = null; string message = string.Empty, notificationList = string.Empty; bool resstate = false, isNotify = false; int noticount = 0;
            int userId = 0;
            string username = "";
            try
            {
                using (_ctx = new FeedBackContext())
                {
                    var User = await _ctx.User.SingleOrDefaultAsync(x => x.UserName == model.UserName && x.Password==model.Password);
                    if (User != null)
                    {
                        userId = User.UserId;
                        username = User.UserName;
                        message = "Login Successfully.";
                        resstate = true;
                    }
                    else
                    {
                        resstate = false;
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return result = new
            {
                userId,
                username,
                message,
                resstate
            };
        }
    }
}
