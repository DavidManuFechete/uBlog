﻿using Microsoft.EntityFrameworkCore;
using uBlog.API.Data;
using uBlog.API.Models.Domain;
using uBlog.API.Repositories.Interface;

namespace uBlog.API.Repositories.Implementation 
{
    public class BlogPostRepository : IBlogPostRepository 
    {

        private readonly ApplicationDbContext _dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext) 
        {
            this._dbContext = dbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost) 
        {
            await _dbContext.BlogPosts.AddAsync(blogPost);
            await _dbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsnyc(Guid id)
        {
            var existingBlogPost = await _dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBlogPost is null) return null;

            _dbContext.BlogPosts.Remove(existingBlogPost);
            await _dbContext.SaveChangesAsync();
            return existingBlogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync() 
        {
            return await _dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id) 
        {
            return await _dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await _dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost) 
        {
            var existingBlogPost = await _dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost == null) return null;

            _dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

            existingBlogPost.Categories = blogPost.Categories;

            await _dbContext.SaveChangesAsync();

            return blogPost;
        }
    }
}
