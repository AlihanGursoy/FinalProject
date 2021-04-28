using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilites.Business;
using Core.Utilites.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }
        //claim
        [SecuredOperation("product.add")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResults Add(Product product)
        {
            IResults result = BusinessRules.Run(CheckhIfProductNameExists(product.ProductName),
                CheckhIfProductOgCategoryCorrect(product.CategoryId),
                CheckhIfCategoryLimitExceded());
            if (result != null)
            {
                return result;
            }


            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour == 22) 
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }
        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResults Update(Product product)
        {
            throw new NotImplementedException();
        }
        [TransactionScopeAspect]
        public IResults AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice<10)
            {
                throw new Exception("HATA");
            }
            Add(product);
            return null;
        }

        //*******************RULES***************************

        private IResults CheckhIfProductOgCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult(Messages.ProductAdded);
        }
        private IResults CheckhIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result == true)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult(Messages.ProductAdded);
        }
        private IResults CheckhIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        
    }
}
