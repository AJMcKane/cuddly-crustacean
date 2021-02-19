using ProductApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Extensions {
    public static class ProductCollectionExtensions {

        public static async Task<IEnumerable<Product>> HighlightProductDescriptions(this IEnumerable<Product> products, IEnumerable<string> highlightWords) {
            var taskList = new List<Task>();
            foreach (var product in products) {
                var task = new Task(() => {
                    foreach (var highlightWord in highlightWords) {
                        product.Description = product.Description.Replace(highlightWord, $"<em>{highlightWord}</em>", StringComparison.InvariantCultureIgnoreCase);
                    }
                });
                task.Start();
                taskList.Add(task);                
            }

            await Task.WhenAll(taskList);            
            return products;
        }
    }
}
