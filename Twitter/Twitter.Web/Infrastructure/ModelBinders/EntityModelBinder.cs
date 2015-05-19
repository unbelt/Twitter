namespace Twitter.Web.Infrastructure.ModelBinders
{
    using System;
    using System.Web.Mvc;

    using Twitter.Contracts;

    public class EntityModelBinder<TEntity> : IModelBinder
        where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> repository;

        public EntityModelBinder(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object value = bindingContext.ValueProvider.GetValue("id").AttemptedValue;

            if (value == null)
            {
                throw new ArgumentNullException();
            }

            int id;

            if (int.TryParse(value.ToString(), out id))
            {
                value = id;
            }

            var model = this.repository.Get(value);

            return model;
        }
    }
}