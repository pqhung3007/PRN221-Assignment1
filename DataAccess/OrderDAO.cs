using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        public OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            List<Order> orderList;
            try
            {
                using (var sale = new Sale_ManagementContext())
                {
                    orderList = sale.Orders.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderList;
        }

        public IEnumerable<Order> GetOrdersByDate(DateTime startDate, DateTime endDate)
        {
            List<Order> orderList;
            try
            {
                using (var sale = new Sale_ManagementContext())
                {
                    orderList = sale.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderList;
        }

        public IEnumerable<Order> GetOrdersByUser(string email)
        {
            List<Order> orderList;
            try
            {
                using (var sale = new Sale_ManagementContext())
                {
                    int memberId = sale.Members.Where(m => m.Email == email).FirstOrDefault().MemberId;
                    orderList = sale.Orders.Where(o => o.MemberId == memberId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderList;
        }

        public Order getOrderById(int id)
        {
            Order order = null;
            try
            {
                using (var sale = new Sale_ManagementContext())
                {
                    order = sale.Orders.SingleOrDefault(o => o.OrderId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public void AddOrder(Order order)
        {
            try
            {
                Order o = getOrderById(order.OrderId);
                if (o == null)
                {
                    using (var sale = new Sale_ManagementContext())
                    {
                        sale.Orders.Add(order);
                        sale.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("This order already exists");
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                Order o = getOrderById(order.OrderId);
                if (o != null)
                {
                    using (var sale = new Sale_ManagementContext())
                    {
                        sale.Entry(order).State = EntityState.Modified;
                        sale.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("This order does not exist");
            }
        }
        public void DeleteOrder(Order order)
        {
            try
            {
                Order o = getOrderById(order.OrderId);
                if (o != null)
                {
                    using (var sale = new Sale_ManagementContext())
                    {
                        sale.Orders.Remove(order);
                        sale.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("This order doesn't exist");
            }
        }
    }
}
