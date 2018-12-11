using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Snmovie.Models;
using Snmovie.Dtos;
using AutoMapper;

namespace Snmovie.Controllers.Api
{
    public class CustomersController : ApiController
    {
    private ApplicationDbContext _context;
    

    public CustomersController()
    {
      _context = new ApplicationDbContext();
    }
    // GET  /Api/customers
    public IHttpActionResult GetCustomers()
    {
      var customerDtos = _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
      return Ok(customerDtos);
    }

    // GET a single customer     /Api/customers/1
    public IHttpActionResult GetCustomer(int Id)
    {
      var customer = _context.Customers.SingleOrDefault(c => c.Id == Id);

      if (customer == null)
      {
        throw new HttpResponseException(HttpStatusCode.NotFound);
      }
      
      return Ok(Mapper.Map<Customer, CustomerDto>(customer));
    }

    // POST  /Api/customers // Create customer like this.  
    [HttpPost]
    public IHttpActionResult CreateCustomer(CustomerDto customerDto)
    {
      // først skal jeg validere objektet
      if (!ModelState.IsValid)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
      // other wise
      _context.Customers.Add(customer); 
      _context.SaveChanges();

      customerDto.Id = customer.Id;

      // retunere vi objektet
      
      return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
    }

    // PUT   /Api/customers/1   //Retunere   
    [HttpPut]
    public IHttpActionResult UpdateCustomer(int Id, CustomerDto customerDto)
    {
      if (!ModelState.IsValid)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

      var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == Id);

      if (customerInDb == null)
      {
        throw new HttpResponseException(HttpStatusCode.NotFound);
      }

      Mapper.Map(customerDto, customerInDb);

      // jeg har ikke brug for det her mere fordi passer de to argumenter som er customerDto og taget object customerInDb
      // den er loaden til vores _context den indeholder name, bithdate osv. 
      //customerInDb.Name = customerDto.Name;
      //customerInDb.Birthdate = customerDto.Birthdate;
      //customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
      //customerInDb.MembershipTypeId = customerDto.MembershipTypeId;

      _context.SaveChanges();

      return Ok();
    }

    // DELETE /Api/customers/1  // Slette 
    [HttpDelete]
    public IHttpActionResult DeleteCustomer(int Id)
    {

      // først skal vi tjekke ind i databasen om kunden eksistere 
      var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == Id);

      if (customerInDb == null)
      {
        throw new HttpResponseException(HttpStatusCode.NotFound);
      }

      _context.Customers.Remove(customerInDb);
      _context.SaveChanges();

      return Ok();
    }
  }
}
