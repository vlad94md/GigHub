﻿using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistance.Repositories;

namespace GigHub.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGenreRepository Genres { get; set; }
        public IFollowingRepository Followings { get; set; }
        public IAttendanceRepository Attendances { get; set; }
        public IGigRepository Gigs { get; set; }
        public INotificationRepository Notifications { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Attendances = new AttendanceRepository(_context);
            Followings = new FollowingRepository(_context);
            Genres = new GenreRepository(_context);
            Notifications = new NotificationRepository(_context);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
