using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.BE
{
	[global::System.Data.Linq.Mapping.TableAttribute(Name = "AccessControl.[User]")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{

		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

		private int _UserId;

		private string _Name;

		private string _Surname;

		private string _Phone;

		private string _Email;

		private string _Pasword;

		private System.DateTime _DateCreated;

		private EntitySet<CarPool> _CarPools;

		private EntitySet<JoinLeaveCarPool> _JoinLeaveCarPools;

		#region Extensibility Method Definitions
		partial void OnLoaded();
		partial void OnValidate(System.Data.Linq.ChangeAction action);
		partial void OnCreated();
		partial void OnUserIdChanging(int value);
		partial void OnUserIdChanged();
		partial void OnNameChanging(string value);
		partial void OnNameChanged();
		partial void OnSurnameChanging(string value);
		partial void OnSurnameChanged();
		partial void OnPhoneChanging(string value);
		partial void OnPhoneChanged();
		partial void OnEmailChanging(string value);
		partial void OnEmailChanged();
		partial void OnPaswordChanging(string value);
		partial void OnPaswordChanged();
		partial void OnDateCreatedChanging(System.DateTime value);
		partial void OnDateCreatedChanged();
		#endregion

		public User()
		{
			this._CarPools = new EntitySet<CarPool>(new Action<CarPool>(this.attach_CarPools), new Action<CarPool>(this.detach_CarPools));
			this._JoinLeaveCarPools = new EntitySet<JoinLeaveCarPool>(new Action<JoinLeaveCarPool>(this.attach_JoinLeaveCarPools), new Action<JoinLeaveCarPool>(this.detach_JoinLeaveCarPools));
			OnCreated();
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Surname", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Surname
		{
			get
			{
				return this._Surname;
			}
			set
			{
				if ((this._Surname != value))
				{
					this.OnSurnameChanging(value);
					this.SendPropertyChanging();
					this._Surname = value;
					this.SendPropertyChanged("Surname");
					this.OnSurnameChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Phone", DbType = "VarChar(12)")]
		public string Phone
		{
			get
			{
				return this._Phone;
			}
			set
			{
				if ((this._Phone != value))
				{
					this.OnPhoneChanging(value);
					this.SendPropertyChanging();
					this._Phone = value;
					this.SendPropertyChanged("Phone");
					this.OnPhoneChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Email", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Pasword", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Pasword
		{
			get
			{
				return this._Pasword;
			}
			set
			{
				if ((this._Pasword != value))
				{
					this.OnPaswordChanging(value);
					this.SendPropertyChanging();
					this._Pasword = value;
					this.SendPropertyChanged("Pasword");
					this.OnPaswordChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DateCreated", DbType = "DateTime2 NOT NULL")]
		public System.DateTime DateCreated
		{
			get
			{
				return this._DateCreated;
			}
			set
			{
				if ((this._DateCreated != value))
				{
					this.OnDateCreatedChanging(value);
					this.SendPropertyChanging();
					this._DateCreated = value;
					this.SendPropertyChanged("DateCreated");
					this.OnDateCreatedChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_CarPool", Storage = "_CarPools", ThisKey = "UserId", OtherKey = "UserId")]
		public EntitySet<CarPool> CarPools
		{
			get
			{
				return this._CarPools;
			}
			set
			{
				this._CarPools.Assign(value);
			}
		}

		[global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_JoinLeaveCarPool", Storage = "_JoinLeaveCarPools", ThisKey = "UserId", OtherKey = "UserId")]
		public EntitySet<JoinLeaveCarPool> JoinLeaveCarPools
		{
			get
			{
				return this._JoinLeaveCarPools;
			}
			set
			{
				this._JoinLeaveCarPools.Assign(value);
			}
		}

		public event PropertyChangingEventHandler PropertyChanging;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}

		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private void attach_CarPools(CarPool entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}

		private void detach_CarPools(CarPool entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}

		private void attach_JoinLeaveCarPools(JoinLeaveCarPool entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}

		private void detach_JoinLeaveCarPools(JoinLeaveCarPool entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
	}
}
