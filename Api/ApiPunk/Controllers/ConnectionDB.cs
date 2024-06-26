﻿using ApiPunk.Classes;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;
using System.Net;
using System.Text;
using System.IO;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;

namespace ApiPunk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommandController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ApplicationDbContext _context;

        public CommandController()
        {
            _connectionString = "Data Source=91.108.241.127,1433;Database=Shop;User Id=sa;Password=4j55w#W^&vEi@5~ci2zTPNZk@nYtCHtd;Encrypt=False;TrustServerCertificate=True;";
            _context = new ApplicationDbContext(_connectionString);
        }

        [HttpPost]
        public async Task<IActionResult> HandleCommand(string command, string parameter = "", string parameter2 = "", string parameter3 = "", string parameter4 = "", string parameter5 = "", string parameter6 = "", string parametr7 = "")
        {
            switch (command)
            {
                case "Registration":
                    {
                        try
                        {
                            var newUser = new User
                            {
                                UserName = parameter,
                                UserSurname = parameter2,
                                UserPatronymic = parameter2,
                                Email = parameter3,
                                PasswordHash = parameter4,
                                RegistrationDate = DateTime.Now // Сохраняет только дату без времени
                            };
                            _context.Users.Add(newUser);
                            await _context.SaveChangesAsync();

                            return Ok(newUser.UserID);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred while saving the entity changes. {ex.Message}");
                            return StatusCode(500, "Internal Server Error");
                        }
                    }

                case "Authorization":
                    {
                        try
                        {
                            var user = await _context.Users
                                .Where(u => u.Email == parameter && u.PasswordHash == parameter2)
                                .FirstOrDefaultAsync();

                            if (user != null)
                            {
                                return Ok(user.UserID);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }

                case "SaveProduct":
                    {
                        // Получаем содержимое запроса в виде строки
                        string requestData = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

                        // Десериализуем содержимое запроса в объект Product
                        Product? product = JsonConvert.DeserializeObject<Product>(requestData);

                        // Сохраняем продукт в базе данных
                        _context.Products.Add(product);
                        await _context.SaveChangesAsync();

                        // Возвращаем подтверждение сохранения продукта
                        return Ok(new List<string> { "Product saved successfully." });
                    }

                case "SendMeAllProduct":
                    {
                        try
                        {
                            // Получаем все продукты из базы данных
                            var products = await _context.Products.ToListAsync();

                            string json = JsonConvert.SerializeObject(products);
                            await System.IO.File.WriteAllTextAsync(@"D:\test\products.json", json);

                            // Возвращаем список продуктов в виде строки Json @"D:\test\products.json"
                            return Ok(@"D:\test\products.json");
                        }
                        catch (Exception ex)
                        {
                            return StatusCode(500, "An error occurred while retrieving products.");
                        }
                    }

                case "AddProductToCart":
                    {
                        int userID = int.Parse(parameter);
                        int productID = int.Parse(parameter2);
                        Order? currentOrder = await _context.Orders.Where(x => x.UserID == userID && x.Status == "В корзине").FirstOrDefaultAsync();

                        if (currentOrder == null)
                        {
                            var order = new Order
                            {
                                UserID = userID,
                                Date = DateTime.Today,
                                Status = "В корзине"
                            };
                            _context.Orders.Add(order);
                            await _context.SaveChangesAsync();
                            currentOrder = order;
                        }

                        if (!parameter3.IsNullOrEmpty())
                        {
                            OrderDetail? currentOrderDetail = await _context.OrderDetails.Where(x => x.OrderID == currentOrder.OrderID && x.ProductID == productID).FirstOrDefaultAsync();
                            if (currentOrderDetail != null)
                            {
                                if (int.Parse(parameter3) > 0)
                                    currentOrderDetail.Quantity = int.Parse(parameter3);
                                else
                                {
                                    _context.OrderDetails.Remove(currentOrderDetail);
                                }
                            }
                            try
                            {
                                await _context.SaveChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            return Ok();
                        }

                        var orderDetail = new OrderDetail
                        {
                            OrderID = currentOrder.OrderID,
                            ProductID = productID,
                            Quantity = 1,
                            Price = _context.Products.Where(x => x.Id == productID).FirstOrDefaultAsync().Result.Price
                        };
                        _context.OrderDetails.Add(orderDetail);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }

                case "EmptyTheTrash":
                    {
                        int userID = int.Parse(parameter);
                        var order = await _context.Orders.Where(x => x.UserID == userID && x.Status == "В корзине").FirstOrDefaultAsync();

                        if (order == null)
                        {
                            return NotFound();
                        }

                        var productsInCart = await _context.OrderDetails
                            .Where(od => od.OrderID == order.OrderID)
                            .ToListAsync();
                        foreach (var product in productsInCart)
                        {
                            _context.OrderDetails.Remove(product);
                        }

                        await _context.SaveChangesAsync();

                        return Ok();
                    }

                case "PlaceAnOrder":
                    {
                        int orderID = int.Parse(parameter);
                        string newOrderStatus = "Оформлен";

                        var order = await _context.Orders
                            .Where(o => o.OrderID == orderID)
                            .FirstOrDefaultAsync();

                        if (order != null)
                        {
                            order.Status = newOrderStatus;
                            order.UserFullName = parameter2;
                            order.UserPhone = parameter3;
                            order.UserEmail = parameter4;
                            order.PaymentMethod = parameter5;
                            order.Address = parameter6;
                            order.DeliveryMethod = parametr7;
                            await _context.SaveChangesAsync();
                        }

                        return Ok();
                    }

                case "SetCardNumber":
                    {
                        int userID = int.Parse(parameter);
                        string newCardNumber = parameter2;

                        var user = await _context.Users
                            .Where(u => u.UserID == userID)
                            .FirstOrDefaultAsync();

                        if (user != null)
                        {
                            user.BankCardNumber = newCardNumber;
                            await _context.SaveChangesAsync();
                        }

                        return Ok();
                    }

                case "ChangeProduct":
                    {
                        int productId = int.Parse(parameter);
                        decimal newPrice = decimal.Parse(parameter2);

                        var product = await _context.Products
                            .Where(p => p.Id == productId)
                            .FirstOrDefaultAsync();

                        if(product != null)
                        {
                            product.Price = newPrice;
                            await _context.SaveChangesAsync();
                        }

                        return Ok();
                    }

                default:
                    return NotFound("FailToWorkRequest");

                    
            }
        }

        [HttpGet("cart/products-in-cart")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetUserProductsInCart(int userID)
        {
            var orderInCart = await _context.Orders
                .Where(x => x.UserID == userID && x.Status == "В корзине")
                .FirstOrDefaultAsync();

            if (orderInCart == null)
            {
                return NotFound();
            }

            var productsInCart = await _context.OrderDetails
                .Where(x => x.OrderID == orderInCart.OrderID)
                .ToListAsync();

            return Ok(productsInCart);
        }

        [HttpGet("cart")]
        public async Task<ActionResult<Order?>> GetUserCart(int userID)
        {
            var cartOrder = await _context.Orders
                .Where(o => o.UserID == userID && o.Status == "В корзине")
                .FirstOrDefaultAsync();

            if (cartOrder == null)
            {
                return NotFound();
            }

            return Ok(cartOrder);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return Ok(products);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving products.");
            }
        }

        [HttpGet("products/product")]
        public async Task<ActionResult<Product>> GetProductById(int productID)
        {
            try
            {
                var product = await _context.Products.Where(p => p.Id == productID).FirstOrDefaultAsync();
                return Ok(product);
            }
            catch
            {

                return StatusCode(500, "An error occurred while retrieving product.");
            }
        }

        [HttpGet("users/user")]
        public async Task<ActionResult<User>> GetUserById(int userID)
        {
            try
            {
                var user = await _context.Users.Where(u => u.UserID == userID).FirstOrDefaultAsync();
                return Ok(user);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
