using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Domain.Responses
{
    public interface IResponseModel
    {
        string Message { get; set; }
        bool DidError { get; set; }
        string ErrorMessage { get; set; }
    }

    public interface ISingleResponse<TModel> : IResponseModel
    {
        TModel Model { get; set; }
    }

    public interface IListResponse<TModel> : IResponseModel
    {
        IEnumerable<TModel> Model { get; set; }
    }

    public interface IPagedResponse<TModel> : IListResponse<TModel>
    {
        int ItemsCount { get; set; }
        double PageCount { get; }
    }
}
