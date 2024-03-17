﻿using Microsoft.AspNetCore.Mvc;
using uBlog.API.Models.Domain;
using uBlog.API.Models.DTO;
using uBlog.API.Repositories.Interface;

namespace uBlog.API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase {

        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository) {
            this._blogPostRepository = blogPostRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request) {

            var blogPost = new BlogPost {

                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
            };

            blogPost = await _blogPostRepository.CreateAsync(blogPost);

            var response = new BlogPostDto {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
            };

            return Ok(response);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts() {

            var blogPosts = await _blogPostRepository.GetAllAsync();
            
            var response = new List<BlogPostDto>();
            foreach(var blogPost in blogPosts) {
                response.Add(new BlogPostDto {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle,
                });
            }
            
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id) {

            var existingBlogPost = await _blogPostRepository.GetById(id);

            if (existingBlogPost is null) return NotFound();

            var response = new BlogPostDto{
                Id = existingBlogPost.Id,
                Author = existingBlogPost.Author,
                Content = existingBlogPost.Content,
                FeaturedImageUrl = existingBlogPost.FeaturedImageUrl,
                IsVisible = existingBlogPost.IsVisible,
                PublishedDate = existingBlogPost.PublishedDate,
                ShortDescription = existingBlogPost.ShortDescription,
                Title = existingBlogPost.Title,
                UrlHandle = existingBlogPost.UrlHandle,
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, UpdateBlogPostRequestDto request) {

            var blogPost = new BlogPost {
               Id = id,
               Author = request.Author,
               Content = request.Content,
               FeaturedImageUrl = request.FeaturedImageUrl,
               IsVisible = request.IsVisible,
               PublishedDate = request.PublishedDate,
               ShortDescription = request.ShortDescription,
               Title = request.Title,
               UrlHandle = request.UrlHandle,
            };

            blogPost = await _blogPostRepository.UpdateAsync(blogPost);

            if (blogPost is null) return NotFound();

            var response = new BlogPostDto {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id) {

            var blogPost = await _blogPostRepository.DeleteAsnyc(id);

            if (blogPost is null) return NotFound();

            var response = new BlogPostDto {
                Id = id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
            };

            return Ok(response);
        }

    }
}
