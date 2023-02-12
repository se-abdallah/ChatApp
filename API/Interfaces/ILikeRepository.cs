using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entity;

namespace API.Interfaces
{
    public interface ILikeRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId , int targetUserId);
        Task<UserLike> GetUserWithLikes(int userId);
        Task<IEnumerable<LikeDto>>  GetUserLikes(string predicate, int userId);
    }
}