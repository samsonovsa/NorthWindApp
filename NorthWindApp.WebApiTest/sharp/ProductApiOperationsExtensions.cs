// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace NorthWind
{
    using Models;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for ProductApiOperations.
    /// </summary>
    public static partial class ProductApiOperationsExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static ProblemDetails Get(this IProductApiOperations operations)
            {
                return operations.GetAsync().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ProblemDetails> GetAsync(this IProductApiOperations operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='productApi'>
            /// </param>
            public static Stream Create(this IProductApiOperations operations, ProductApi productApi)
            {
                return operations.CreateAsync(productApi).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='productApi'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Stream> CreateAsync(this IProductApiOperations operations, ProductApi productApi, CancellationToken cancellationToken = default(CancellationToken))
            {
                var _result = await operations.CreateWithHttpMessagesAsync(productApi, null, cancellationToken).ConfigureAwait(false);
                _result.Request.Dispose();
                return _result.Body;
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static ProblemDetails Get2(this IProductApiOperations operations, int id)
            {
                return operations.Get2Async(id).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ProblemDetails> Get2Async(this IProductApiOperations operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.Get2WithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='productApi'>
            /// </param>
            public static Stream Update(this IProductApiOperations operations, int id, ProductApi productApi)
            {
                return operations.UpdateAsync(id, productApi).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='productApi'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Stream> UpdateAsync(this IProductApiOperations operations, int id, ProductApi productApi, CancellationToken cancellationToken = default(CancellationToken))
            {
                var _result = await operations.UpdateWithHttpMessagesAsync(id, productApi, null, cancellationToken).ConfigureAwait(false);
                _result.Request.Dispose();
                return _result.Body;
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static Stream Delete(this IProductApiOperations operations, int id)
            {
                return operations.DeleteAsync(id).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Stream> DeleteAsync(this IProductApiOperations operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                var _result = await operations.DeleteWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false);
                _result.Request.Dispose();
                return _result.Body;
            }

    }
}
