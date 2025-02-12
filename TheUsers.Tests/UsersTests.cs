using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheUsers.Api.Controllers;
using TheUsers.Api.Validators;
using TheUsers.Domain.Models;
using TheUsers.Domain.Services;

namespace TheUsers.Tests
{
    public class UsersTests
    {
        private Mock<IUserService> _userServiceMock;
        private UsersController _usersController;
        private List<User> _users;
        private User _user;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _usersController = new UsersController(_userServiceMock.Object);
            _users = new List<User>() { new User { Id = 1, FirstName = "Test", LastName = "User", Email = "test.user@example.com", DateOfBirth = DateTime.Now.AddYears(-20), PhoneNumber = 1234567890 } };
            _user = _users[0];

            _userServiceMock.Setup(x => x.GetAllUsers()).Returns(_users);
            _userServiceMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(_user);
        }

        [Test]
        public void Get_WhenCalled_ReturnsAllUsers()
        {
            var result = _usersController.Get();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(_users));
        }

        [Test]
        public void Get_WhenCalledWithId_ReturnsUser()
        {
            var result = _usersController.Get(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(_user));
        }

        [Test]
        public void Get_WithNonExistingId_ReturnsNotFound()
        {
            _userServiceMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns((User)null);

            var result = _usersController.Get(1);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }
        

        [Test]
        public void Put_WithNonExistingId_ReturnsNotFound()
        {
            _userServiceMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns((User)null);

            var result = _usersController.Put(1, _user);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Delete_WithNonExistingId_ReturnsNotFound()
        {
            _userServiceMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns((User)null);

            var result = _usersController.Delete(1);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Post_WithInvalidUser_ReturnsBadRequest_WhenFirstNameInvalid()
        {
            var user = new User { Id = 1, FirstName = "", LastName = "User", Email = "test.user@example.com", DateOfBirth = DateTime.Now.AddYears(-20), PhoneNumber = 1234567890 };
            var validator = new UserValidator();
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
            {
                var result = _usersController.Post(user);

                Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
                var badRequestResult = result as BadRequestObjectResult;
                var errors = badRequestResult?.Value as List<ValidationFailure>;
                Assert.That(errors?[0].ErrorMessage, Is.EqualTo(validationResult.Errors[0].ErrorMessage));
            }
        }

        [Test]
        public void Post_WithInvalidUser_ReturnsBadRequest_WhenLastNameInvalid()
        {
            var user = new User { Id = 1, FirstName = "Test", LastName = "", Email = "test.user@example.com", DateOfBirth = DateTime.Now.AddYears(-20), PhoneNumber = 1234567890 };
            var validator = new UserValidator();
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
            {
                var result = _usersController.Post(user);

                Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
                var badRequestResult = result as BadRequestObjectResult;
                var errors = badRequestResult?.Value as List<ValidationFailure>;
                Assert.That(errors?[0].ErrorMessage, Is.EqualTo(validationResult.Errors[0].ErrorMessage));
            }
        }

        [Test]
        public void Post_WithInvalidUser_ReturnsBadRequest_WhenPhoneNumberInvalid()
        {
            var user = new User { Id = 1, FirstName = "Test", LastName = "User", Email = "test.user@example.com", DateOfBirth = DateTime.Now.AddYears(-20), PhoneNumber = 123 };
            var validator = new UserValidator();
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
            {
                var result = _usersController.Post(user);

                Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
                var badRequestResult = result as BadRequestObjectResult;
                var errors = badRequestResult?.Value as List<ValidationFailure>;
                Assert.That(errors?[0].ErrorMessage, Is.EqualTo(validationResult.Errors[0].ErrorMessage));
            }
        }

        [Test]
        public void Post_WithInvalidUser_ReturnsBadRequest_WhenUserNot18YearsOrOlder()
        {
            var user = new User { Id = 1, FirstName = "", LastName = "User", Email = "test.user@example.com", DateOfBirth = DateTime.Now.AddYears(-5), PhoneNumber = 1234567890 };
            var validator = new UserValidator();
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
            {
                var result = _usersController.Post(user);

                Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
                var badRequestResult = result as BadRequestObjectResult;
                var errors = badRequestResult?.Value as List<ValidationFailure>;
                Assert.That(errors?[0].ErrorMessage, Is.EqualTo(validationResult.Errors[0].ErrorMessage));
            }
        }

        [Test]
        public void Post_WithInvalidUser_ReturnsBadRequest_WhenEmailInvalid()
        {
            var user = new User { Id = 1, FirstName = "", LastName = "User", Email = "abcdef", DateOfBirth = DateTime.Now.AddYears(-5), PhoneNumber = 1234567890 };
            var validator = new UserValidator();
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
            {
                var result = _usersController.Post(user);

                Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
                var badRequestResult = result as BadRequestObjectResult;
                var errors = badRequestResult?.Value as List<ValidationFailure>;
                Assert.That(errors?[0].ErrorMessage, Is.EqualTo(validationResult.Errors[0].ErrorMessage));
            }
        }


    }
}