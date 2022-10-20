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
	[global::System.Data.Linq.Mapping.TableAttribute(Name = "Config.JoinLeaveCarPool")]
	public partial class JoinLeaveCarPool : INotifyPropertyChanging, INotifyPropertyChanged
	{

		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

		private int _JoinLeaveCarPoolId;

		private int _UserId;

		private int _CarPoolId;

		private string _OwnerLeader;

		private System.Nullable<bool> _IsActive;

		private System.DateTime _DateAdded;

		private EntityRef<CarPool> _CarPool;

		private EntityRef<User> _User;

		#region Extensibility Method Definitions
		partial void OnLoaded();
		partial void OnValidate(System.Data.Linq.ChangeAction action);
		partial void OnCreated();
		partial void OnJoinLeaveCarPoolIdChanging(int value);
		partial void OnJoinLeaveCarPoolIdChanged();
		partial void OnUserIdChanging(int value);
		partial void OnUserIdChanged();
		partial void OnCarPoolIdChanging(int value);
		partial void OnCarPoolIdChanged();
		partial void OnOwnerLeaderChanging(string value);
		partial void OnOwnerLeaderChanged();
		partial void OnIsActiveChanging(System.Nullable<bool> value);
		partial void OnIsActiveChanged();
		partial void OnDateAddedChanging(System.DateTime value);
		partial void OnDateAddedChanged();
		#endregion

		public JoinLeaveCarPool()
		{
			this._CarPool = default(EntityRef<CarPool>);
			this._User = default(EntityRef<User>);
			OnCreated();
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_JoinLeaveCarPoolId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
		public int JoinLeaveCarPoolId
		{
			get
			{
				return this._JoinLeaveCarPoolId;
			}
			set
			{
				if ((this._JoinLeaveCarPoolId != value))
				{
					this.OnJoinLeaveCarPoolIdChanging(value);
					this.SendPropertyChanging();
					this._JoinLeaveCarPoolId = value;
					this.SendPropertyChanged("JoinLeaveCarPoolId");
					this.OnJoinLeaveCarPoolIdChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserId", DbType = "Int NOT NULL")]
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
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CarPoolId", DbType = "Int NOT NULL")]
		public int CarPoolId
		{
			get
			{
				return this._CarPoolId;
			}
			set
			{
				if ((this._CarPoolId != value))
				{
					if (this._CarPool.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCarPoolIdChanging(value);
					this.SendPropertyChanging();
					this._CarPoolId = value;
					this.SendPropertyChanged("CarPoolId");
					this.OnCarPoolIdChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OwnerLeader", DbType = "VarChar(50)")]
		public string OwnerLeader
		{
			get
			{
				return this._OwnerLeader;
			}
			set
			{
				if ((this._OwnerLeader != value))
				{
					this.OnOwnerLeaderChanging(value);
					this.SendPropertyChanging();
					this._OwnerLeader = value;
					this.SendPropertyChanged("OwnerLeader");
					this.OnOwnerLeaderChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsActive", DbType = "Bit")]
		public System.Nullable<bool> IsActive
		{
			get
			{
				return this._IsActive;
			}
			set
			{
				if ((this._IsActive != value))
				{
					this.OnIsActiveChanging(value);
					this.SendPropertyChanging();
					this._IsActive = value;
					this.SendPropertyChanged("IsActive");
					this.OnIsActiveChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DateAdded", DbType = "DateTime2 NOT NULL")]
		public System.DateTime DateAdded
		{
			get
			{
				return this._DateAdded;
			}
			set
			{
				if ((this._DateAdded != value))
				{
					this.OnDateAddedChanging(value);
					this.SendPropertyChanging();
					this._DateAdded = value;
					this.SendPropertyChanged("DateAdded");
					this.OnDateAddedChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.AssociationAttribute(Name = "CarPool_JoinLeaveCarPool", Storage = "_CarPool", ThisKey = "CarPoolId", OtherKey = "CarPoolId", IsForeignKey = true)]
		public CarPool CarPool
		{
			get
			{
				return this._CarPool.Entity;
			}
			set
			{
				CarPool previousValue = this._CarPool.Entity;
				if (((previousValue != value)
							|| (this._CarPool.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._CarPool.Entity = null;
						previousValue.JoinLeaveCarPools.Remove(this);
					}
					this._CarPool.Entity = value;
					if ((value != null))
					{
						value.JoinLeaveCarPools.Add(this);
						this._CarPoolId = value.CarPoolId;
					}
					else
					{
						this._CarPoolId = default(int);
					}
					this.SendPropertyChanged("CarPool");
				}
			}
		}

		[global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_JoinLeaveCarPool", Storage = "_User", ThisKey = "UserId", OtherKey = "UserId", IsForeignKey = true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value)
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.JoinLeaveCarPools.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.JoinLeaveCarPools.Add(this);
						this._UserId = value.UserId;
					}
					else
					{
						this._UserId = default(int);
					}
					this.SendPropertyChanged("User");
				}
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
	}
}
