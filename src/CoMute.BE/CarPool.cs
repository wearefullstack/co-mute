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

	[global::System.Data.Linq.Mapping.TableAttribute(Name = "Config.CarPool")]
	public partial class CarPool : INotifyPropertyChanging, INotifyPropertyChanged
	{

		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

		private int _CarPoolId;

		private int _UserId;

		private System.DateTime _DepartureTime;

		private System.DateTime _ExpectedArrivalTime;

		private string _Origin;

		private int _DaysAvailable;

		private string _Destination;

		private int _AvailableSeats;

		private string _OwnerLeader;

		private string _Notes;

		private System.DateTime _DateCreated;

		private EntitySet<JoinLeaveCarPool> _JoinLeaveCarPools;

		private EntityRef<User> _User;

		#region Extensibility Method Definitions
		partial void OnLoaded();
		partial void OnValidate(System.Data.Linq.ChangeAction action);
		partial void OnCreated();
		partial void OnCarPoolIdChanging(int value);
		partial void OnCarPoolIdChanged();
		partial void OnUserIdChanging(int value);
		partial void OnUserIdChanged();
		partial void OnDepartureTimeChanging(System.DateTime value);
		partial void OnDepartureTimeChanged();
		partial void OnExpectedArrivalTimeChanging(System.DateTime value);
		partial void OnExpectedArrivalTimeChanged();
		partial void OnOriginChanging(string value);
		partial void OnOriginChanged();
		partial void OnDaysAvailableChanging(int value);
		partial void OnDaysAvailableChanged();
		partial void OnDestinationChanging(string value);
		partial void OnDestinationChanged();
		partial void OnAvailableSeatsChanging(int value);
		partial void OnAvailableSeatsChanged();
		partial void OnOwnerLeaderChanging(string value);
		partial void OnOwnerLeaderChanged();
		partial void OnNotesChanging(string value);
		partial void OnNotesChanged();
		partial void OnDateCreatedChanging(System.DateTime value);
		partial void OnDateCreatedChanged();
		#endregion

		public CarPool()
		{
			this._JoinLeaveCarPools = new EntitySet<JoinLeaveCarPool>(new Action<JoinLeaveCarPool>(this.attach_JoinLeaveCarPools), new Action<JoinLeaveCarPool>(this.detach_JoinLeaveCarPools));
			this._User = default(EntityRef<User>);
			OnCreated();
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CarPoolId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
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
					this.OnCarPoolIdChanging(value);
					this.SendPropertyChanging();
					this._CarPoolId = value;
					this.SendPropertyChanged("CarPoolId");
					this.OnCarPoolIdChanged();
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

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DepartureTime", DbType = "DateTime2 NOT NULL")]
		public System.DateTime DepartureTime
		{
			get
			{
				return this._DepartureTime;
			}
			set
			{
				if ((this._DepartureTime != value))
				{
					this.OnDepartureTimeChanging(value);
					this.SendPropertyChanging();
					this._DepartureTime = value;
					this.SendPropertyChanged("DepartureTime");
					this.OnDepartureTimeChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExpectedArrivalTime", DbType = "DateTime2 NOT NULL")]
		public System.DateTime ExpectedArrivalTime
		{
			get
			{
				return this._ExpectedArrivalTime;
			}
			set
			{
				if ((this._ExpectedArrivalTime != value))
				{
					this.OnExpectedArrivalTimeChanging(value);
					this.SendPropertyChanging();
					this._ExpectedArrivalTime = value;
					this.SendPropertyChanged("ExpectedArrivalTime");
					this.OnExpectedArrivalTimeChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Origin", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Origin
		{
			get
			{
				return this._Origin;
			}
			set
			{
				if ((this._Origin != value))
				{
					this.OnOriginChanging(value);
					this.SendPropertyChanging();
					this._Origin = value;
					this.SendPropertyChanged("Origin");
					this.OnOriginChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DaysAvailable", DbType = "Int NOT NULL")]
		public int DaysAvailable
		{
			get
			{
				return this._DaysAvailable;
			}
			set
			{
				if ((this._DaysAvailable != value))
				{
					this.OnDaysAvailableChanging(value);
					this.SendPropertyChanging();
					this._DaysAvailable = value;
					this.SendPropertyChanged("DaysAvailable");
					this.OnDaysAvailableChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Destination", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Destination
		{
			get
			{
				return this._Destination;
			}
			set
			{
				if ((this._Destination != value))
				{
					this.OnDestinationChanging(value);
					this.SendPropertyChanging();
					this._Destination = value;
					this.SendPropertyChanged("Destination");
					this.OnDestinationChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AvailableSeats", DbType = "Int NOT NULL")]
		public int AvailableSeats
		{
			get
			{
				return this._AvailableSeats;
			}
			set
			{
				if ((this._AvailableSeats != value))
				{
					this.OnAvailableSeatsChanging(value);
					this.SendPropertyChanging();
					this._AvailableSeats = value;
					this.SendPropertyChanged("AvailableSeats");
					this.OnAvailableSeatsChanged();
				}
			}
		}

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OwnerLeader", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
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

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Notes", DbType = "VarChar(50)")]
		public string Notes
		{
			get
			{
				return this._Notes;
			}
			set
			{
				if ((this._Notes != value))
				{
					this.OnNotesChanging(value);
					this.SendPropertyChanging();
					this._Notes = value;
					this.SendPropertyChanged("Notes");
					this.OnNotesChanged();
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

		[global::System.Data.Linq.Mapping.AssociationAttribute(Name = "CarPool_JoinLeaveCarPool", Storage = "_JoinLeaveCarPools", ThisKey = "CarPoolId", OtherKey = "CarPoolId")]
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

		[global::System.Data.Linq.Mapping.AssociationAttribute(Name = "User_CarPool", Storage = "_User", ThisKey = "UserId", OtherKey = "UserId", IsForeignKey = true)]
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
						previousValue.CarPools.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.CarPools.Add(this);
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

		private void attach_JoinLeaveCarPools(JoinLeaveCarPool entity)
		{
			this.SendPropertyChanging();
			entity.CarPool = this;
		}

		private void detach_JoinLeaveCarPools(JoinLeaveCarPool entity)
		{
			this.SendPropertyChanging();
			entity.CarPool = null;
		}
	}
}
