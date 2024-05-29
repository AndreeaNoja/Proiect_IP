using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CommaSeparatedModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult != ValueProviderResult.None)
        {
            var value = valueProviderResult.FirstValue;

            if (!string.IsNullOrEmpty(value))
            {
                var values = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var floatList = new List<float>();
                foreach (var item in values)
                {
                    if (float.TryParse(item, out var floatValue))
                    {
                        floatList.Add(floatValue);
                    }
                    else
                    {
                        bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid float value.");
                        return Task.CompletedTask;
                    }
                }

                bindingContext.Result = ModelBindingResult.Success(floatList);
                return Task.CompletedTask;
            }
        }

        bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "No value provided.");
        return Task.CompletedTask;
    }
}
