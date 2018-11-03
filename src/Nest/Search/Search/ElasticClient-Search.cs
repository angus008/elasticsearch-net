﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The search API allows to execute a search query and get back search hits that match the query.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-search.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="selector">A descriptor that describes the parameters for the search operation</param>
		ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null) where T : class;

		/// <inheritdoc />
		ISearchResponse<T> Search<T>(ISearchRequest request) where T : class;

		/// <inheritdoc />
		ISearchResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector = null)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		ISearchResponse<TResult> Search<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="selector">A descriptor that describes the parameters for the search operation</param>
		Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class;

		/// <inheritdoc />
		Task<ISearchResponse<T>> SearchAsync<T>(ISearchRequest request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

		/// <inheritdoc />
		Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(ISearchRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class
			where TResult : class;
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null) where T : class =>
			Search<T, T>(selector);

		/// <inheritdoc />
		public ISearchResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector = null)
			where T : class
			where TResult : class =>
			Search<TResult>(selector.InvokeOrDefault(new SearchDescriptor<T>()));

		/// <inheritdoc />
		public ISearchResponse<T> Search<T>(ISearchRequest request) where T : class =>
			Search<T, T>(request);

		/// <inheritdoc />
		public ISearchResponse<TResult> Search<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class =>
			Dispatcher.Dispatch<ISearchRequest, SearchRequestParameters, SearchResponse<TResult>>(
				request,
				(p, d) => LowLevelDispatch.SearchDispatch<SearchResponse<TResult>>(p, d)
			);

		/// <inheritdoc />
		public Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class =>
			SearchAsync<T, T>(selector, cancellationToken);

		/// <inheritdoc />
		public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class
			where TResult : class =>
			SearchAsync<TResult>(selector.InvokeOrDefault(new SearchDescriptor<T>()), cancellationToken);

		/// <inheritdoc />
		public Task<ISearchResponse<T>> SearchAsync<T>(ISearchRequest request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			SearchAsync<T, T>(request, cancellationToken);

		/// <inheritdoc />
		public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(ISearchRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class
			where TResult : class =>
			Dispatcher.DispatchAsync<ISearchRequest, SearchRequestParameters, SearchResponse<TResult>, ISearchResponse<TResult>>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.SearchDispatchAsync<SearchResponse<TResult>>(p, d, c)
			);
	}
}
