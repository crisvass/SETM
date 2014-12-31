using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common;
using Common.CustomExceptions;
using Common.Views;
using DataAccessLayer;

namespace BusinessLayer_WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductCategoriesService" in both code and config file together.
    public class ProductCategoriesService : IProductCategoriesService
    {
        public IEnumerable<CategoryView> GetCategories()
        {
            try
            {
                return new CategoriesRepository().GetCategories();
            }
            catch
            {
                throw new FaultException("Error whilst retrieving categories. Please contact administrator if error persists.");
            }
        }

        public string GetCategoryName(Guid categoryId)
        {
            try
            {
                return new CategoriesRepository().GetCategoryName(categoryId);
            }
            catch
            {
                throw new FaultException("Error whilst retrieving categories. Please contact administrator if error persists.");
            }
        }

        public CategoryView GetCategory(Guid id)
        {
            try
            {
                CategoriesRepository cr = new CategoriesRepository();
                CategoryView category = cr.GetCategoryView(id);
                category.SubCategories = cr.GetSubCategories(id);
                return category;
            }
            catch
            {
                throw new FaultException("Error whilst retrieving category. Please contact administrator if error persists.");
            }
        }

        public void AddCategory(string categoryName, List<CategoryView> subCategories)
        {
            try
            {
                CategoriesRepository cr = new CategoriesRepository();
                Guid id = Guid.NewGuid();
                ProductCategory pc = new ProductCategory()
                {
                    Id = id,
                    Name = categoryName
                };

                try
                {
                    cr.Entity.Database.Connection.Open();
                    cr.Transaction = cr.Entity.Database.BeginTransaction();

                    cr.AddCategory(pc);
                    foreach (CategoryView sub in subCategories)
                        cr.AddCategory(new ProductCategory()
                        {
                            Id = Guid.NewGuid(),
                            Name = sub.CategoryName,
                            ParentId = id
                        });

                    cr.Transaction.Commit();
                }
                catch
                {
                    cr.Transaction.Rollback();
                    throw new TransactionFailedException("Adding a new category failed. Please try again or contact administrator if error persists.");
                }
                finally
                {
                    cr.Entity.Database.Connection.Close();
                }
            }
            catch (TransactionFailedException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("Error whilst adding category. Please contact administrator if error persists.");
            }
        }

        public void UpdateCategory(Guid categoryId, string categoryName, List<CategoryView> subCategories)
        {
            try
            {
                CategoriesRepository cr = new CategoriesRepository();
                ProductCategory pc = new ProductCategory()
                {
                    Id = categoryId,
                    Name = categoryName
                };

                try
                {
                    cr.Entity.Database.Connection.Open();
                    cr.Transaction = cr.Entity.Database.BeginTransaction();
                    List<CategoryView> subcategoriesOriginal = cr.GetSubCategories(categoryId).ToList<CategoryView>();

                    cr.UpdateCategory(pc);

                    bool found;

                    //delete removed subcategories
                    foreach (CategoryView cv in subcategoriesOriginal)
                    {
                        found = false;
                        foreach (CategoryView category in subCategories)
                        {
                            if (cv.CategoryId == category.CategoryId)
                            {
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            cr.DeleteCategory(cv.CategoryId);
                        }
                    }

                    //add new submenus
                    foreach (CategoryView cv in subCategories)
                    {
                        found = false;
                        foreach (CategoryView category in subcategoriesOriginal)
                        {
                            if (cv.CategoryId == category.CategoryId)
                            {
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            cr.AddCategory(new ProductCategory()
                            {
                                Id = Guid.NewGuid(),
                                Name = cv.CategoryName,
                                ParentId = categoryId
                            });
                        }
                    }

                    cr.Transaction.Commit();
                }
                catch
                {
                    cr.Transaction.Rollback();
                    throw new TransactionFailedException("Updating category failed. Please try again or contact administrator if error persists.");
                }
                finally
                {
                    cr.Entity.Database.Connection.Close();
                }
            }
            catch (TransactionFailedException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("Error whilst updating category. Please contact administrator if error persists.");
            }
        }

        public void DeleteCategory(Guid id)
        {
            try
            {
                CategoriesRepository cr = new CategoriesRepository();
                if (!(cr.CategoryIsUsed(id) && cr.SubCategoriesAreUsed(id)))
                {
                    try
                    {
                        cr.Entity.Database.Connection.Open();
                        cr.Transaction = cr.Entity.Database.BeginTransaction();

                        cr.DeleteSubCategories(id);
                        cr.DeleteCategory(id);

                        cr.Transaction.Commit();
                    }
                    catch
                    {
                        cr.Transaction.Rollback();
                        throw new TransactionFailedException("Category Deletion Failed. Please try again or contact administrator if error persists.");
                    }
                    finally
                    {
                        cr.Entity.Database.Connection.Close();
                    }
                }
                else
                {
                    throw new ConstraintException("Category cannot be deleted because the category or one of its subcategories are assigned to a product.");
                }
            }
            catch (TransactionFailedException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (ConstraintException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("Error whilst retrieving categories. Please contact administrator if error persists.");
            }
        }
    }
}
