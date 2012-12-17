Hypersonic
==========

“Auto-Mapper for the database”


Hypersonic is a lightweight data access framework for .Net. It makes working with ADO.Net and stored procedures a breeze.

Hypersonic is a convention base mapper. It handles data going both directions, to and from the database. Going to the database, the property names must match the stored procedure parameters. Coming from the database the column names must match the property names.

**Requirments**
- .NET 4.0 Framework or better


**Limitations**

- Properties names must be unique across the class hierarchy.
- Multipule ResultSets are not supported.
- DataSets are not supported.
- DataTables are not supported, except for passing Table-Valued Parameters to SQL Server.
- Enums are converted to their numeric value, not to their string value.


**Getting Started**

Configure you connection string in the web.config.

**Setting the Connection String**

<pre style='color:#000000;background:#ffffff;'><span style='color:#a65700; '>&lt;</span><span style='color:#5f5035; '>connectionStrings</span><span style='color:#a65700; '>></span>
      <span style='color:#a65700; '>&lt;</span><span style='color:#5f5035; '>clear</span><span style='color:#a65700; '>/></span>
        <span style='color:#a65700; '>&lt;</span><span style='color:#5f5035; '>add</span> <span style='color:#274796; '>connectionString</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"</span><span style='color:#0000e6; '>Data Source=localhost;Initial Catalog=Development.ThePhotoProject;User Id=joe;Password=cat;</span><span style='color:#0000e6; '>"</span> <span style='color:#274796; '>name</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"</span><span style='color:#0000e6; '>SqlServer</span><span style='color:#0000e6; '>"</span> <span style='color:#a65700; '>/></span>
<span style='color:#a65700; '>&lt;/</span><span style='color:#5f5035; '>connectionStrings</span><span style='color:#a65700; '>></span>
</pre>

By default, if a connection string is not set in code it will use the first connection string in the configuration section. Many times default is set in a machine config. By using 'clear' it will clear out all connection strings set in the machine.config.


**Capturing Out Parameters**

<pre>
			IDatabase database = new MsSqlDatabase();
			
			DbParameter outParameter = database.MakeParameter("@Identity", 0, ParameterDirection.Output);

			List<DbParameter> parameters = new List<DbParameter> 
			{
					database.MakeParameter("@FirstName",user.FirstName),
					database.MakeParameter("@LastName",user.LastName),
					database.MakeParameter("@Password",user.Password),
					database.MakeParameter("@Email",user.Email),
					database.MakeParameter("@DisplayName",user.DisplayName),
					database.MakeParameter("@Deleted",user.Deleted),
					database.MakeParameter("@Username",user.Username),
					database.MakeParameter("@AccountStatus",AccountStatus.Public),
					outParameter
			};

			database.NonQuery("User_Insert", parameters);
			int userId = Convert.ToInt32(outParameter.Value);

			return userId;
			
</pre>


**Auto Generating Parameters**

Hypersonic can decompose and generate the parameters based on the names of the properties. 

    User user = new User {Id=1, Name="John Doe", Password="secret"};

    IDatabase database = new MsSqlDatabase();
    return database.NonQuery("User_Update", user);


**Aliasing Property Names**

Not all properties will have the same name as their column counterparts, for properties that do not match the column name, a data alias attribute can be used to denote the name of the column in the database


    /// <summary>
    /// Gets or sets the user id.
    /// </summary>
    /// <value>The user id.</value>
    [DataAlias(Alias = "UserId")]
    public int Id { get; set; }
  
**Ignoring Parameters**

Not all properties are meant to be populated in the database. For those properties that should be ignored when saving to the database there is an attribute. Simply annotate the property with ‘IgnoreParameter’ and the property and it’s value will not be extracted from the object.


    /// <summary>
    /// Gets or sets the comment count.
    /// </summary>
    /// <value>The comment count.</value>
    [Ignore]
    public int CommentCount { get; set; }
    Retrieve Single Value with AutoPopulate

    /// <summary>
    /// Retrieves the by primary key.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns></returns>
    public User RetrieveByPrimaryKey(int userId)
    {
        User user = database.Single("User_SelectByPrimaryKey", new { userId }, database.AutoPopulate<User>);
        return user;
    }
    
**Retrieving Multiple Values with AutoPopulate**


    /// <summary>
    /// Retrieves the comments by user id.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns></returns>
    public List<Comment> RetrieveCommentsByUserId(int userId)
    {
        return database.List("Comment_RetrieveCommentsByUserId", new { userId }, database.AutoPopulate<Comment>);
    }


Defining a method entails have one parameter of type INullableReader and the return type of the a single object type. Collections are not allowed.


<pre>
/// <summary>
/// Populates the specified reader.
/// </summary>
/// <param name="reader">The reader.</param>
/// <returns></returns>
internal static User Populate(INullableReader reader)
{
User user = new User
		  {
			  Id = reader.GetInt32("UserId"),
			  FirstName = reader.GetString("FirstName"),
			  LastName = reader.GetString("LastName"),
			  Password = reader.GetString("Password"),
			  Email = reader.GetString("Email"),
			  DisplayName = reader.GetString("DisplayName"),
			  Deleted = reader.GetBoolean("Deleted"),
			  Username = reader.GetString("Username"),
			  AccountStatus = reader.GetString("AccountStatus").ParseEnum<AccountStatus>(),
			  Settings = new UserSettings
							 {
								 EnableReceivingOfEmails = reader.GetBoolean("EnableReceivingOfEmails"),
								 WebViewMaxHeight = reader.GetInt16("WebViewMaxHeight"),
								 WebViewMaxWidth = reader.GetInt16("WebViewMaxWidth")
							 }

		  };

return user;
}
</pre>		


**Using the Mapping**

Simply pass the Mapping Method, in this case it’s called ‘Populate’, into the last parameter of the Populate methods.

<pre>
/// <summary>
/// Retrieves the user by username.
/// </summary>
/// <param name="username">The username.</param>
/// <returns></returns>
public User RetrieveUserByUsername(string username)
{
	return database.Single("User_RetrieveByUsername", new { username }, Populate);
}
</pre>		
		
**Anonymous Types as Parameters**

Anonymous Types can be used to pass parameters. Below mediaId and userId are passed into the NonQuery method by using an Anonymous Type. This is can happen because Anonymous Types compile to a class.


<pre>

/// <summary>
/// Delete a Media by the primary key
/// </summary>
/// <param name="mediaId">The media id.</param>
/// <param name="userId">The user id.</param>
/// <returns></returns>
public int Delete(int mediaId, int userId)
{
	return database.NonQuery("Media_Delete", new{mediaId, userId});
}
</pre>

