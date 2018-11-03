using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FieldSecurity>))]
	public interface IFieldSecurity
	{
		[JsonProperty("except")]
		Fields Except { get; set; }

		[JsonProperty("grant")]
		Fields Grant { get; set; }
	}

	public class FieldSecurity : IFieldSecurity
	{
		public Fields Except { get; set; }
		public Fields Grant { get; set; }
	}

	public class FieldSecurityDescriptor<T> : DescriptorBase<FieldSecurityDescriptor<T>, IFieldSecurity>, IFieldSecurity
		where T : class
	{
		Fields IFieldSecurity.Except { get; set; }
		Fields IFieldSecurity.Grant { get; set; }

		public FieldSecurityDescriptor<T> Grant(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Grant = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public FieldSecurityDescriptor<T> Grant(Fields fields) => Assign(a => a.Grant = fields);

		public FieldSecurityDescriptor<T> Except(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Except = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public FieldSecurityDescriptor<T> Except(Fields fields) => Assign(a => a.Except = fields);
	}
}
