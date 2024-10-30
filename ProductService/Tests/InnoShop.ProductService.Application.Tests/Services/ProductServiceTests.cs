using AutoMapper;
using InnoShop.ProductService.Domain.RepositoryInterfaces;
using Moq;

namespace InnoShop.ProductService.Application.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Application.Services.ProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
        _productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllProductsAsync_ShouldReturnMappedProducts()
    {
        // Arrange
        var productsDb = new List<Product> { new Product(), new Product() };
        var expectedProducts = new List<GetAllProductModel> { new(), new() };

        _productRepositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(productsDb);

        _mapperMock.Setup(x => x.Map<IEnumerable<GetAllProductModel>>(productsDb))
            .Returns(expectedProducts);

        // Act
        var result = await _productService.GetAllProductsAsync();

        // Assert
        result.Should().BeEquivalentTo(expectedProducts);
        _productRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        _mapperMock.Verify(x => x.Map<IEnumerable<GetAllProductModel>>(productsDb), Times.Once);
    }

    [Fact]
    public async Task SearchProductsByNameAsync_ShouldReturnMappedProducts()
    {
        // Arrange
        var productName = "TestProduct";
        var productsDb = new List<Product> { new Product(), new Product() };
        var expectedProducts = new List<ProductSearchModel> { new(), new() };

        _productRepositoryMock.Setup(x => x.SearchProductsByNameAsync(productName))
            .ReturnsAsync(productsDb);

        _mapperMock.Setup(x => x.Map<IEnumerable<ProductSearchModel>>(productsDb))
            .Returns(expectedProducts);

        // Act
        var result = await _productService.SearchProductsByNameAsync(productName);

        // Assert
        result.Should().BeEquivalentTo(expectedProducts);
        _productRepositoryMock.Verify(x => x.SearchProductsByNameAsync(productName), Times.Once);
        _mapperMock.Verify(x => x.Map<IEnumerable<ProductSearchModel>>(productsDb), Times.Once);
    }

    [Theory]
    [InlineData(SortFieldEnum.Name, SortOrderEnum.Ascending)]
    [InlineData(SortFieldEnum.Name, SortOrderEnum.Descending)]
    [InlineData(SortFieldEnum.Price, SortOrderEnum.Ascending)]
    [InlineData(SortFieldEnum.Price, SortOrderEnum.Descending)]
    public async Task SortProductsByFieldAsync_ShouldReturnSortedProducts(SortFieldEnum sortField, SortOrderEnum sortOrder)
    {
        // Arrange
        var productsDb = new List<Product> { new Product(), new Product() };
        var products = new List<ProductSortingModel>
            {
                new() { Name = "B", Price = 200 },
                new() { Name = "A", Price = 100 }
            };

        _productRepositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(productsDb);

        _mapperMock.Setup(x => x.Map<IEnumerable<ProductSortingModel>>(productsDb))
            .Returns(products);

        // Act
        var result = await _productService.SortProductsByFieldAsync(sortField, sortOrder);

        // Assert
        if (sortField == SortFieldEnum.Name)
        {
            if (sortOrder == SortOrderEnum.Ascending)
                result.Should().BeInAscendingOrder(x => x.Name);
            else
                result.Should().BeInDescendingOrder(x => x.Name);
        }
        else // Price
        {
            if (sortOrder == SortOrderEnum.Ascending)
                result.Should().BeInAscendingOrder(x => x.Price);
            else
                result.Should().BeInDescendingOrder(x => x.Price);
        }
    }

    [Fact]
    public async Task CreateProductAsync_ShouldCreateProduct()
    {
        // Arrange
        var model = new ProductCreationModel();
        var userId = "testUserId";
        var product = new Product();

        _mapperMock.Setup(x => x.Map<Product>(model))
            .Returns(product);

        // Act
        await _productService.CreateProductAsync(model, userId);

        // Assert
        product.UserId.Should().Be(userId);
        _productRepositoryMock.Verify(x => x.AddAsync(product), Times.Once);
    }

    [Fact]
    public async Task EditProductAsync_WhenProductNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var model = new ProductModificationModel { Id = Guid.NewGuid() };
        var userId = "testUserId";

        _productRepositoryMock.Setup(x => x.GetByIdAsync(model.Id))
            .ReturnsAsync((Product)null);

        // Act & Assert
        await _productService.Invoking(x => x.EditProductAsync(model, userId))
            .Should().ThrowAsync<Exception>()
            .WithMessage("Product not found.");
    }

    [Fact]
    public async Task EditProductAsync_WhenUserIsNotOwner_ShouldThrowForbiddenException()
    {
        // Arrange
        var model = new ProductModificationModel { Id = Guid.NewGuid() };
        var userId = "testUserId";
        var product = new Product { UserId = "differentUserId" };

        _productRepositoryMock.Setup(x => x.GetByIdAsync(model.Id))
            .ReturnsAsync(product);

        // Act & Assert
        await _productService.Invoking(x => x.EditProductAsync(model, userId))
            .Should().ThrowAsync<Exception>()
            .WithMessage("This user can't interact with this product.");
    }

    [Fact]
    public async Task DeleteProductAsync_WhenProductNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var model = new ProductDeletionModel { Id = Guid.NewGuid() };
        var userId = "testUserId";

        _productRepositoryMock.Setup(x => x.GetByIdAsync(model.Id))
            .ReturnsAsync((Product)null);

        // Act & Assert
        await _productService.Invoking(x => x.DeleteProductAsync(model, userId))
            .Should().ThrowAsync<Exception>()
            .WithMessage("Product not found.");
    }

    [Fact]
    public async Task DeleteProductAsync_WhenUserIsNotOwner_ShouldThrowForbiddenException()
    {
        // Arrange
        var model = new ProductDeletionModel { Id = Guid.NewGuid() };
        var userId = "testUserId";
        var product = new Product { UserId = "differentUserId" };

        _productRepositoryMock.Setup(x => x.GetByIdAsync(model.Id))
            .ReturnsAsync(product);

        // Act & Assert
        await _productService.Invoking(x => x.DeleteProductAsync(model, userId))
            .Should().ThrowAsync<Exception>()
            .WithMessage("This user can't interact with this product.");
    }

    [Fact]
    public async Task DeleteProductAsync_WhenValidRequest_ShouldDeleteProduct()
    {
        // Arrange
        var model = new ProductDeletionModel { Id = Guid.NewGuid() };
        var userId = "testUserId";
        var product = new Product { UserId = userId };

        _productRepositoryMock.Setup(x => x.GetByIdAsync(model.Id))
            .ReturnsAsync(product);

        // Act
        await _productService.DeleteProductAsync(model, userId);

        // Assert
        _productRepositoryMock.Verify(x => x.DeleteAsync(product), Times.Once);
    }

    [Fact]
    public async Task EditProductAsync_WhenValidRequest_ShouldUpdateProduct()
    {
        // Arrange
        var model = new ProductModificationModel
        {
            Id = Guid.NewGuid(),
            Name = "Updated Name",
            Description = "Updated Description",
            Price = 99.99m,
            IsAvailable = false
        };
        var userId = "testUserId";
        var existingProduct = new Product { UserId = userId };

        _productRepositoryMock.Setup(x => x.GetByIdAsync(model.Id))
            .ReturnsAsync(existingProduct);

        // Act
        await _productService.EditProductAsync(model, userId);

        // Assert
        existingProduct.Name.Should().Be(model.Name);
        existingProduct.Description.Should().Be(model.Description);
        existingProduct.Price.Should().Be(model.Price);
        existingProduct.IsAvailable.Should().Be(model.IsAvailable);
        _productRepositoryMock.Verify(x => x.ModifyAsync(existingProduct), Times.Once);
    }

    [Fact]
    public async Task SearchProductsBySubstringAsync_ShouldReturnMappedProducts()
    {
        // Arrange
        var substring = "Test";
        var productsDb = new List<Product> { new Product(), new Product() };
        var expectedProducts = new List<ProductSearchModel> { new(), new() };

        _productRepositoryMock.Setup(x => x.SearchProductsBySubstringAsync(substring))
            .ReturnsAsync(productsDb);

        _mapperMock.Setup(x => x.Map<IEnumerable<ProductSearchModel>>(productsDb))
            .Returns(expectedProducts);

        // Act
        var result = await _productService.SearchProductsBySubstringAsync(substring);

        // Assert
        result.Should().BeEquivalentTo(expectedProducts);
        _productRepositoryMock.Verify(x => x.SearchProductsBySubstringAsync(substring), Times.Once);
        _mapperMock.Verify(x => x.Map<IEnumerable<ProductSearchModel>>(productsDb), Times.Once);
    }

    [Fact]
    public async Task SortProductsByFieldAsync_WithInvalidSortField_ShouldThrowArgumentException()
    {
        // Arrange
        var invalidSortField = (SortFieldEnum)999; // Invalid enum value
        var sortOrder = SortOrderEnum.Ascending;
        var productsDb = new List<Product> { new Product() };
        var products = new List<ProductSortingModel> { new() };

        _productRepositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(productsDb);

        _mapperMock.Setup(x => x.Map<IEnumerable<ProductSortingModel>>(productsDb))
            .Returns(products);

        // Act & Assert
        await _productService.Invoking(x => x.SortProductsByFieldAsync(invalidSortField, sortOrder))
            .Should().ThrowAsync<ArgumentException>()
            .WithMessage("Invalid sort field (Parameter 'sortField')");
    }
}