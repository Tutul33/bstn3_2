using feedback.Models;
using feedback.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace feedback.Management
{
    public class CommentMgt
    {
        FeedBackContext _ctx = null;
        public async Task<object> createComment(Comment model)
        {
            object result = null; string message = string.Empty; bool resstate = false;
            using (_ctx = new FeedBackContext())
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {

                        if (model.CommentId > 0)
                        {
                            var objComment = await _ctx.Comment.FirstOrDefaultAsync(x => x.CommentId == model.CommentId);
                            objComment.CommentText = model.CommentText;
                            objComment.CreationTime = DateTime.Now;
                            objComment.PostId = model.PostId;
                            objComment.UserId = model.UserId;
                        }
                        else
                        {
                            var objComment = new Comment();
                            objComment.CommentText = model.CommentText;
                            objComment.CreationTime = DateTime.Now;
                            objComment.PostId = model.PostId;
                            objComment.UserId = model.UserId;
                            if (objComment != null)
                            {
                                await _ctx.Comment.AddAsync(objComment);
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
        public async Task<object> setOpinion(vmCommonParam model)
        {
            object result = null; string message = string.Empty; bool resstate = false;
            using (_ctx = new FeedBackContext())
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {

                        if (model.id > 0)
                        {
                            var objComment = await _ctx.Comment.FirstOrDefaultAsync(x => x.CommentId == model.id);
                            var objOpinionLog = await _ctx.OpinionLog.FirstOrDefaultAsync(x => x.CommentId == model.id && x.UserId == model.UserId);
                            if (objOpinionLog != null)
                            { 
                                if (objOpinionLog.IsLike != model.opinion)
                                {
                                    objComment.CDislike = !model.opinion ? (Convert.ToInt32(objComment.CDislike) + 1) : (objComment.CDislike > 0 ? (Convert.ToInt32(objComment.CDislike) - 1) : 0);

                                    objComment.CLike = model.opinion ? (Convert.ToInt32(objComment.CLike) + 1) : (objComment.CLike > 0 ? (Convert.ToInt32(objComment.CLike) - 1) : 0);
                                }
                                objOpinionLog.IsLike = model.opinion;
                            }
                            else
                            {
                                var opinionLog = new OpinionLog();
                                opinionLog.CommentId = model.id;
                                opinionLog.UserId = model.UserId;
                                opinionLog.IsLike = model.opinion;
                                if (opinionLog != null)
                                {
                                    await _ctx.OpinionLog.AddAsync(opinionLog);
                                }
                                objComment.CDislike = !model.opinion ? (Convert.ToInt32(objComment.CDislike) + 1) : Convert.ToInt32(objComment.CDislike);
                                objComment.CLike = model.opinion ? (Convert.ToInt32(objComment.CLike) + 1) : Convert.ToInt32(objComment.CLike);
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
    }
}
