﻿using Microsoft.AspNetCore.Http;
using StudyJunction.Core.RequestDTOs.Lecture;
using StudyJunction.Core.ResponseDTOs;
using StudyJunction.Core.ViewModels.Lectures;

namespace StudyJunction.Core.Services.Contracts
{
    public interface ILectureService
	{
		Task<LectureResponseDTO> Get(Guid id);
        Task<LectureResponseDTO> Get(string title);
        Task<string> GetAssignmentId(string lectureTitle);
        Task<LectureViewModel> GetAllLecturesOfCourse(string courseTitle);
        Task<LectureViewModel> GetAllLecturesAndSetTargetLectureAsCurrent(string lectureTitle);
        Task<ICollection<LectureResponseDTO>> GetAll();
		Task<LectureResponseDTO> Create(AddLectureRequestDto newLecture, string username);
		Task<LectureResponseDTO> CreateWithVideoAndAssignmentFromViewModel(AddLectureViewModel model, string username);

        Task<LectureResponseDTO> AddAssignmentAsync(string lectureId, IFormFile assignment, string userId);
        Task<LectureResponseDTO> Update(Guid toUpdate, LectureRequestDto newData, string username);
		Task<LectureResponseDTO> Delete(Guid id, string username);
	}
}
