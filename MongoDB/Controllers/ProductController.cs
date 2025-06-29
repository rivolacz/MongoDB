using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Models;

namespace MongoDB.Controllers;
[Route("")]
public class ProductController : Controller
{
    private readonly IMongoCollection<Product> _products;

    public ProductController(IMongoClient client)
    {
        var database = client.GetDatabase("appdb");
        _products = database.GetCollection<Product>("items");
    }

    [HttpGet]
    public IActionResult Index()
    {
        var products = _products.Find(_ => true).ToList();
        return View(products);
    }

    [HttpGet("Create")]
    public IActionResult Create() => View();

    [HttpPost("Create")]
    public IActionResult Create(Product product)
    {
        _products.InsertOne(product);
        return RedirectToAction("Index");
    }
}