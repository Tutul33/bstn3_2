using feedback.db;
using feedback.Models;
using feedback.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace feedback.Management
{
    public class PostMgt
    {
        FeedBackContext _ctx = null;
        Hashtable ht = null;
        private IGenericFactory<vmPost> Generic_vmPost = null;
        public async Task<object> CreatePost(Post model)
        {
            object result = null; string message = string.Empty; bool resstate = false;
            using (_ctx = new FeedBackContext())
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {

                        if (model.PostId > 0)
                        {
                            var objPost = await _ctx.Post.FirstOrDefaultAsync(x => x.PostId == model.PostId);
                            objPost.PostText = model.PostText;
                            objPost.CreationTime = DateTime.Now;
                            objPost.UserId = model.UserId;
                            
                        }
                        else
                        {
                            var objPost = new Post();
                            objPost.PostText = model.PostText;
                            objPost.CreationTime = DateTime.Now;
                            objPost.UserId = model.UserId;

                            if (objPost != null)
                            {
                                await _ctx.Post.AddAsync(objPost);
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
        public async Task<object> GetPostData(vmCommonParam cmnParam)
        {
            object result = null; string listPost = string.Empty;
            try
            {
                Generic_vmPost = new GenericFactory<vmPost>();

                ht = new Hashtable
                {                   
                   { "pageNumber", cmnParam.pageNumber },
                   { "pageSize", cmnParam.pageSize },
                   { "search", cmnParam.search },
                };

                listPost = await Generic_vmPost.ExecuteCommandString(dataUtilities.SpGetPostData, ht, dataUtilities.conString.ToString());
            }
            catch (Exception ex)
            {
//Logs.WriteBug(ex);
            }

            return result = new
            {
                listPost
            };
        }
        /// <summary>
        /// This method returns an object from database as object with pagination using asynchronous operation by vmCmnParameters class as parameter.
        /// </summary>
        /// <param name="cmnParam"></param>
        /// <returns></returns>
        //public async Task<object> GetWithPage(int cmnParam)
        //{
        //    List<vmFeedback> listUser = null;
        //    object result = null; int recordsTotal = 0;
        //    try
        //    {
        //        using (_ctx = new FeedBackContext())
        //        {

        //            listUser = await (from u in _ctx.User
        //                              join p in _ctx.Post on u.UserId equals p.UserId
        //                              join c in _ctx.Comment on p.PostId equals c.PostId
        //                              //where u.UserRoleId != StaticInfos.SeedRoleID
        //                              select new vmFeedback
        //                              {
        //                                  UserId = u.UserId,
        //                                  PostId= p.PostId

        //                              }).ToListAsync();


        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return result = new
        //    {
        //        listUser,
        //        recordsTotal
        //    };
        //}
    }
}
