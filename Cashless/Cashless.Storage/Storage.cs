using System.Collections.Generic;
using Cashless.Domain.Card.Class;
using Cashless.Domain.Data.Class;

namespace Cashless.Storage
{
    public static class Storage
    {
        public static List<Vendor> Vendors { get; set; }
        public static List<Customer> Customers { get; set; }
        public static List<Purchase> Purchases { get; set; } = new List<Purchase>();
        public static List<Transfer> Transfers { get; set; } = new List<Transfer>();
        public static List<SavedCard> Cards { get; set; } = new List<SavedCard>();

        static Storage()
        {
            Vendors = new List<Vendor>()
            {
                new Vendor() {Id = "VENDOR1", Name = "Comida Japonesa", TokenQuantity = 100},
                new Vendor() {Id = "VENDOR2", Name = "Hamburguer", TokenQuantity = 200},
                new Vendor() {Id = "VENDOR3", Name = "Lembrancinhas", TokenQuantity = 1_000},
            };

            Customers = new List<Customer>()
            {
                new Customer() {Id = "CUS1", Name = "Joao Endinheirado", TokenQuantity = 1_000_000},
                new Customer() {Id = "CUS2", Name = "José Novo Cliente", TokenQuantity = 0},
            };
        }

        public static ApplicationStatus GetStatus()
        {
            return new ApplicationStatus()
            {
                Transfers = Transfers,
                Customers = Customers,
                Purchases = Purchases,
                Vendors = Vendors
            };
        }
    }
}
