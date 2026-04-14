using LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Books.Commands;
using LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Members.Commands;
using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.LibraryManagementSystem.Domain.Entities;
using AutoMapper;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces.Profile
{
    public class MappingProfiles : AutoMapper.Profile  
    {
        public MappingProfiles()
        {
            CreateMap<Member, MemberDto>();
            CreateMap<CreateMemberCommand, Member>();
            CreateMap<UpdateMemberCommand, Member>();
         
            CreateMap<Book, BookDto>();
            CreateMap<CreateBookCommand, Book>();
            CreateMap<UpdateBookCommand, Book>();

            CreateMap<BorrowingRecord, BorrowingDto>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
        }
    }
}


