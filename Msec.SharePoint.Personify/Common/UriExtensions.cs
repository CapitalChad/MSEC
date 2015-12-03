using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Msec.Collections;

namespace Msec {
	/// <summary>
	/// Provides extension methods for the <see cref="T:Uri"/> class.  This class may not be inherited.
	/// </summary>
	public static class UriExtensions {
	// Methods
		/// <summary>
		/// Appends the path specified to the URI.
		/// </summary>
		/// <param name="instance">The URI to which to append the relative path.</param>
		/// <param name="relativePath">The relative path to append.</param>
		/// <returns>The newly created <see cref="T:Uri"/> object.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="relativePath"/> is a null reference.</exception>
		/// <exception cref="System.UriFormatException">The URI constructed by adding <paramref name="relativePath"/> would result in an invalid URI.</exception>
		public static Uri Append(this Uri instance, String relativePath) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (relativePath == null) {
				throw new ArgumentNullException("relativePath");
			}

			UriBuilder builder = new UriBuilder(instance);

			String path = builder.Path;
			if (!path.EndsWith("/", StringComparison.Ordinal)) {
				path = path + "/";
			}
			if (relativePath.StartsWith("/", StringComparison.Ordinal)) {
				relativePath = relativePath.Substring(1);
			}
			path = path + relativePath;
			builder.Path = path;

			return builder.Uri;
		}
		/// <summary>
		/// Appends the query information specified to a URI.
		/// </summary>
		/// <param name="instance">The URI to which to append the query information.</param>
		/// <param name="key">The key to add.</param>
		/// <param name="value">The value for the key to add.</param>
		/// <returns>The new URI created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="key"/> is a null reference.
		/// -or- <paramref name="value"/> is a null reference.</exception>
		/// <exception cref="System.UriFormatException">The URI constructed by adding <paramref name="key"/> and <paramref name="value"/> would result in an invalid URI.</exception>
		/// <exception cref="Msec.Collections.DuplicateKeyException"><paramref name="key"/> already exists in the query.</exception>
		public static Uri AppendQuery(this Uri instance, String key, String value) {
			return UriExtensions.AppendQuery(instance, key, value, false);
		}
		/// <summary>
		/// Appends the query information specified to a URI.
		/// </summary>
		/// <param name="instance">The URI to which to append the query information.</param>
		/// <param name="key">The key to add.</param>
		/// <param name="value">The value for the key to add.</param>
		/// <param name="overwrite"><c>true</c> if an existing value should be overwritten in the event that <paramref name="key"/> already exists; otherwise, <c>false</c>.</param>
		/// <returns>The new URI created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="key"/> is a null reference.
		/// -or- <paramref name="value"/> is a null reference.</exception>
		/// <exception cref="System.UriFormatException">The URI constructed by adding <paramref name="key"/> and <paramref name="value"/> would result in an invalid URI.</exception>
		/// <exception cref="Msec.Collections.DuplicateKeyException"><paramref name="key"/> already exists in the query and <paramref name="overwrite"/> is <c>false</c>.</exception>
		public static Uri AppendQuery(this Uri instance, String key, String value, Boolean overwrite) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (key == null) {
				throw new ArgumentNullException("key");
			}
			if (value == null) {
				throw new ArgumentNullException("value");
			}

			UriBuilder builder = new UriBuilder(instance);

			IDictionary<String, String> queryDictionary = UriExtensions.CreateQueryDictionary(builder.Query);
			if (queryDictionary.ContainsKey(key)) {
				if (!overwrite) {
					throw new DuplicateKeyException("The key specified already exists in the query.");
				}
				queryDictionary.Remove(key);
			}
			queryDictionary.Add(key, value);

			String query = UriExtensions.CreateQuery(queryDictionary);
			builder.Query = query;
			
			return builder.Uri;
		}
		/// <summary>
		/// Returns a value indicating if the URI contains the key specified in the query information.
		/// </summary>
		/// <param name="instance">The URI to check.</param>
		/// <param name="key">The key for which to check.</param>
		/// <returns><c>true</c> if the key is contained; otherwise, <c>false</c>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="key"/> is a null reference.</exception>
		public static Boolean ContainsQueryKey(this Uri instance, String key) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (key == null) {
				throw new ArgumentNullException("key");
			}
			IDictionary<String, String> queryDictionary = UriExtensions.CreateQueryDictionary(instance.Query);
			return queryDictionary.ContainsKey(key);
		}
		/// <summary>
		/// Creates and returns query information for the query dictionary specified.
		/// </summary>
		/// <param name="queryDictionary">Contains query information.</param>
		/// <returns>The query string created.</returns>
		private static String CreateQuery(IDictionary<String, String> queryDictionary) {
			Debug.Assert(queryDictionary != null);
			String[] values = queryDictionary
				.Select(pair => new String[] { pair.Key, pair.Value })
				.Select(pair => pair.Join("="))
				.ToArray();
			String query = values.Join("&");
			return query;
		}
		/// <summary>
		/// Creates and returns a dictionary representing the query information supplied.
		/// </summary>
		/// <param name="query">The query string to parse.</param>
		/// <returns>A dictionary containing the keys and values in the query string.</returns>
		private static IDictionary<String, String> CreateQueryDictionary(String query) {
			IDictionary<String, String> queryDictionary = new Dictionary<String, String>();
			if (!String.IsNullOrEmpty(query)) {
				if (query.StartsWith("?", StringComparison.Ordinal)) {
					query = query.Substring(1);
				}
				String[] values = query.Split('&');
				foreach (String value in values) {
					String[] pair = value.Split('=');
					Debug.Assert(pair.Length == 2);
					queryDictionary.Add(pair[0], pair[1]);
				}
			}
			return queryDictionary;
		}
		/// <summary>
		/// Removes the key specified from the query string if it exists.
		/// </summary>
		/// <param name="instance">The URI to which to append the relative path.</param>
		/// <param name="key">The key to remove.</param>
		/// <returns>The <see cref="T:Uri"/> created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="key"/> is a null reference.</exception>
		public static Uri RemoveQuery(this Uri instance, String key) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (key == null) {
				throw new ArgumentNullException("key");
			}

			UriBuilder builder = new UriBuilder(instance);

			IDictionary<String, String> queryDictionary = UriExtensions.CreateQueryDictionary(builder.Query);
			if (queryDictionary.ContainsKey(key)) {
				queryDictionary.Remove(key);
				String query = UriExtensions.CreateQuery(queryDictionary);
				builder.Query = query;
				return builder.Uri;
			}
			return instance;
		}
	}
}
