using Blog.Business.Abstract;
using Blog.DataAccess.Abstract;
using Blog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Business.Concrete
{
    public class CommentManager : GenericManager<Comment>, ICommentService
    {
        private readonly IGenericDal<Comment> _genericDal;
        private readonly ICommentDal _commentDal;

        public CommentManager(IGenericDal<Comment> genericDal,
            ICommentDal commentDal) : base(genericDal)
        {
            _genericDal = genericDal;
            _commentDal = commentDal;
        }

        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int topicId, int? parentId)
        {
            return await _commentDal.GetAllWithSubCommentsAsync(topicId, parentId);
        }
    }
}
