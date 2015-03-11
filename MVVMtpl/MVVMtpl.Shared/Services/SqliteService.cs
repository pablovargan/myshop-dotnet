namespace MVVMtpl.Services
{
    using MVVMtpl.Models;
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Windows.Storage;

    public class SqliteService
    {
        private static string dbPath = "orders.db";

        public async Task<ObservableCollection<Order>> LoadOrders()
        {
            bool dbExist = await CheckDbAsync(dbPath);
            if (!dbExist)
            {
                await CreateDatabaseAsync();
            }

            var orders = new ObservableCollection<Order>();
            SQLiteAsyncConnection conn;
            try
            {
                conn = new SQLiteAsyncConnection(dbPath);
                var ordersList = await conn.Table<Order>().ToListAsync();
                var ordersLineList = await conn.Table<OrderLine>().ToListAsync();
                var productsList = await conn.Table<Product>().ToListAsync();

                foreach(var ol in ordersLineList) 
                {
                    foreach (var p in productsList)
                    {
                        if (ol.Id.Equals(p.Id))
                        {
                            ol.Product = p;
                        }
                    }
                }

                foreach (var order in ordersList)
                {
                    order.OrderLines = new List<OrderLine>();
                    foreach (var ol in ordersLineList)
                    {
                        if (order.Id.Equals(ol.OrderId))
                        {
                            order.OrderLines.Add(ol);
                        }
                    }
                    orders.Add(order);
                }
            }
            catch (Exception)
            {
                return new ObservableCollection<Order>();
            }
            finally
            {
                conn = null;
            }
            return orders;
        }

        public async Task<bool> SaveOrder(Order order)
        {
            bool success = false;

            var conexion = new SQLiteAsyncConnection(dbPath);

            SQLiteAsyncConnection conn;
            try
            {
                conn = new SQLiteAsyncConnection(dbPath);
                var orderStorage = await conn.Table<Order>().Where(x => x.Id == order.Id).FirstOrDefaultAsync();
                if (orderStorage == null)
                {
                    foreach(var p in order.OrderLines)
                    {
                        await conn.InsertAsync(p.Product);
                    }
                    await conn.InsertAllAsync(order.OrderLines);
                    var result = await conn.InsertAsync(order);
                    success = result == 1;
                }
            }
            catch (Exception)
            {
                return success;
            }
            finally
            {
                conn = null;
            }

            return success;
        }

        private async Task<bool> CheckDbAsync(string dbName)
        {
            bool dbExist = true;
            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(dbName);
            }
            catch (Exception)
            {
                dbExist = false;
            }

            return dbExist;
        }

        private async Task CreateDatabaseAsync()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbPath);
            await conn.CreateTableAsync<Order>();
            await conn.CreateTableAsync<OrderLine>();
            await conn.CreateTableAsync<Product>();
            conn = null;
        }

        public async Task ClearDataBaseAsync()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbPath);
            await conn.DropTableAsync<Order>();
            await conn.CreateTableAsync<Order>();
            await conn.DropTableAsync<OrderLine>();
            await conn.CreateTableAsync<OrderLine>();
            await conn.DropTableAsync<Product>();
            await conn.CreateTableAsync<Product>();
            conn = null;
        }
    }
}
