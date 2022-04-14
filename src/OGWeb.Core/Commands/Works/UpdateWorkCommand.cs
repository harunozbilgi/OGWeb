using AutoMapper;
using MediaBalansDocument.Library;
using MediaBalansDocument.Library.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Entities;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Core.Wrappers;

namespace OGWeb.Core.Commands.Works;

public class UpdateWorkCommand : IRequest<CustomResponse<NoContent>>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Keyword_Seo { get; set; }
    public string Description_Seo { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool? IsActived { get; set; }
    public List<IFormFile> Files { get; set; }

    public class UpdateWorkCommandHandler : IRequestHandler<UpdateWorkCommand, CustomResponse<NoContent>>
    {
        private readonly IWriteRepositoryManager _writeRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateWorkCommandHandler> _logger;

        public UpdateWorkCommandHandler(IWriteRepositoryManager writeRepositoryManager, IMapper mapper, ILogger<UpdateWorkCommandHandler> logger)
        {
            _writeRepositoryManager = writeRepositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomResponse<NoContent>> Handle(UpdateWorkCommand request, CancellationToken cancellationToken)
        {
            string path = "files/works/";

            if (request.Id == Guid.Empty)
            {
                _logger.LogError($"Querying for single workId: {request.Id}");
                throw new ArgumentNullException($"Querying for single workId: {request.Id}");
            }
            var work = _mapper.Map<Work>(request);


            if (request.Files != null && request.Files.Count > 0)
            {
                foreach (var item in request.Files)
                {
                    var response_file = await FileUploader.UploadAsync(item, path);

                    string image_Url = string.Concat(path, response_file.DocumentName);

                    await _writeRepositoryManager.WorkRepository.CreateWorkFileAsync(new WorkFile() { WorkId = request.Id, ImageUrl = image_Url });

                    await ImageHelper.OptimizeAsync(image_Url);
                }
            }
            await _writeRepositoryManager.WorkRepository.UpdateWorkAsync(work);

            return CustomResponse<NoContent>.Success(204);
        }
    }
}