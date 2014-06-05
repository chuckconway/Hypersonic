using System.Collections.Generic;
using Hypersonic.Core.Interception;

namespace Hypersonic.Core
{
    /// <summary>
    /// Class HypersonicSettings.
    /// </summary>
   public class HypersonicSettings
   {
       /// <summary>
       /// Initializes a new instance of the <see cref="HypersonicSettings"/> class.
       /// </summary>
       public HypersonicSettings()
       {
           PropertySaveInterceptors = new List<IPropertySaveInterceptor>();   
           PropertyMaterializeInterceptors = new List<IPropertyMaterializeInterceptor>();
           ClassMaterializeInterceptors = new List<IClassMaterializeInterceptor>();
           ClassSaveInterceptors = new List<IClassSaveInterceptor>();
           CommandType = HypersonicCommandType.StoredProcedures;
       }

       public HypersonicCommandType CommandType { get; set; }

       /// <summary>
       /// Gets or sets the connection string.
       /// </summary>
       /// <value>The connection string.</value>
       public string ConnectionString { get; set; }

       /// <summary>
       /// Gets or sets the name of the connection string.
       /// </summary>
       /// <value>The name of the connection string.</value>
        public string ConnectionStringName { get; set; }

       /// <summary>
       /// Gets or sets the property save interceptors.
       /// </summary>
       /// <value>The property save interceptors.</value>
       public List<IPropertySaveInterceptor> PropertySaveInterceptors { get; set; }

       /// <summary>
       /// Gets or sets the property materialize interceptors.
       /// </summary>
       /// <value>The property materialize interceptors.</value>
       public List<IPropertyMaterializeInterceptor> PropertyMaterializeInterceptors { get; set; }

       /// <summary>
       /// Gets or sets the class materialize interceptors.
       /// </summary>
       /// <value>The class materialize interceptors.</value>
       public List<IClassMaterializeInterceptor> ClassMaterializeInterceptors { get; set; }

       /// <summary>
       /// Gets or sets the class save interceptors.
       /// </summary>
       /// <value>The class save interceptors.</value>
       public List<IClassSaveInterceptor> ClassSaveInterceptors { get; set; }
   }

    public enum HypersonicCommandType
    {
        StoredProcedures,
        Sql
    }
}
