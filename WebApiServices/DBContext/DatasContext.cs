﻿using Microsoft.EntityFrameworkCore;
using SharedService.Models.Card;
using SharedService.Models.Contact;
using SharedService.Models.PaymentDetail;

namespace SharedService.DBContext
{
    public class DatasContext : DbContext
    {
        public DatasContext(DbContextOptions<DatasContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<CardModel> Cards { get; set; }
        public DbSet<PaymentDetailModel> PaymentDetails { get; set; }
        public DbSet<ContactModel> Contacts { get; set; }
    }
}