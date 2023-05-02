﻿using Moq;
using ProductsApi.Products;
using System.Xml.Linq;

namespace ProductsApi.UnitTests.SlugGeneration;

public class GeneratingSlugs
{
    [Theory]
    [InlineData("Dandruff Shampoo", "dandruff-shampoo")]
    [InlineData("Taco Salad", "taco-salad")]
    [InlineData("Hamburger", "hamburger")]
    [InlineData("Dandruff   Shampoo", "dandruff-shampoo")]
    public async Task CanGenerateSlugs(string name, string expectedSlug)
    {
        var alwaysUniqueChecker = new Mock<ICheckForUniqueValues>();
        alwaysUniqueChecker.Setup(u => u.IsUniqueAsync(It.IsAny<string>())).ReturnsAsync(true);
        
        IGenerateSlugs generator = new SlugGenerator(alwaysUniqueChecker.Object);

        var slug = await generator.GenerateSlugForAsync(name);

        Assert.Equal(expectedSlug, slug);
    }

    [Theory]
    [InlineData("Coffee Beans", "coffee-beans-a")]
    [InlineData("Burrito", "burrito-x")]
    public async Task DuplicatesHaveSuffix(string name, string expected)
    {
        var neverUniqueChecker = new Mock<ICheckForUniqueValues>();
        neverUniqueChecker.Setup(u => u.IsUniqueAsync(expected)).ReturnsAsync(true);

        IGenerateSlugs generator = new SlugGenerator(neverUniqueChecker.Object);

        var slug = await generator.GenerateSlugForAsync(name);

        Assert.Equal(expected, slug);
    }

    [Fact]
    public async Task IfUniqueNameIsntAvailableGenerateGuid()
    {
        var neverUniqueChecker = new Mock<ICheckForUniqueValues>();

        IGenerateSlugs generator = new SlugGenerator(neverUniqueChecker.Object);

        var slug = await generator.GenerateSlugForAsync("Ba Da");

        Assert.StartsWith("ba-da", slug);
        Assert.Equal(41, slug.Length);
    }
}
    