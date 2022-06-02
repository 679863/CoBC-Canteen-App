using System;
using System.Windows.Input;
using Xamarin.Forms;
using CoBCCanteen.Views;
using Xamarin.CommunityToolkit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace CoBCCanteen.ViewModels
{
	public class RegisterPageViewModel : BindableObject
	{
		public ICommand RegisterUser { get; }
		public ICommand DisplayLogin { get; }

		public RegisterPageViewModel()
		{
			RegisterUser = new Command(async() => await OnRegister());
			DisplayLogin = new Command(async() => await GoToLogin());
		}

		private string _firstname;
		public string Firstname
        {
			get => _firstname;
            set
            {
                if (value == _firstname)
                {
					return;
                }
                else
                {
					_firstname = value;
					OnPropertyChanged();
                }
            }
        }

		private string _lastname;
		public string Lastname
		{
			get => _lastname;
			set
			{
				if (value == _lastname)
				{
					return;
				}
				else
				{
					_lastname = value;
					OnPropertyChanged();
				}
			}
		}

		private string _id;
		public string ID
		{
			get => _id;
			set
			{
				if (value == _id)
				{
					return;
				}
				else
				{
					_id = value;
					OnPropertyChanged();
				}
			}
		}

		private string _email;
		public string Email
		{
			get => _email;
			set
			{
				if (value == _email)
				{
					return;
				}
				else
				{
					_email = value;
					OnPropertyChanged();
				}
			}
		}

		private string _password;
		public string Password
		{
			get => _password;
			set
			{
				if (value == _password)
				{
					return;
				}
				else
				{
					_password = value;
					OnPropertyChanged();
				}
			}
		}

		private bool _isFirstnameValid;
		public bool IsFirstnameValid
        {
			get => _isFirstnameValid;
			set
            {
				_isFirstnameValid = value;
				OnPropertyChanged();
            }
        }

		private bool _isLastnameValid;
		public bool IsLastnameValid
		{
			get => _isLastnameValid;
			set
			{
				_isLastnameValid = value;
				OnPropertyChanged();
			}
		}

		private bool _isIDValid;
		public bool IsIDValid
		{
			get => _isIDValid;
			set
			{
				_isIDValid = value;
				OnPropertyChanged();
			}
		}

		private bool _isEmailValid;
		public bool IsEmailValid
		{
			get => _isEmailValid;
			set
			{
				_isEmailValid = value;
				OnPropertyChanged();
			}
		}

		private bool _isPasswordValid;
		public bool IsPasswordValid
		{
			get => _isPasswordValid;
			set
			{
				_isPasswordValid = value;
				OnPropertyChanged();
			}
		}

		private bool _isPasswordConfirmValid;
		public bool IsPasswordConfirmValid
		{
			get => _isPasswordConfirmValid;
			set
			{
				_isPasswordConfirmValid = value;
				OnPropertyChanged();
			}
		}

		private List<object> _errorFirstname;
		public List<object> errorFirstname
		{
			get => _errorFirstname;
			set
            {
                if (value != null)
                {
					_errorFirstname = value;
                }
            }
		}

		private List<object> _errorLastname;
		public List<object> errorLastname
		{
			get => _errorLastname;
			set
			{
				if (value != null)
				{
					_errorLastname = value;
				}
			}
		}

		private List<object> _errorID;
		public List<object> errorID
		{
			get => _errorID;
			set
			{
				if (value != null)
				{
					_errorID = value;
				}
			}
		}

		private List<object> _errorEmail;
		public List<object> errorEmail
		{
			get => _errorEmail;
			set
			{
				if (value != null)
				{
					_errorEmail = value;
				}
			}
		}

		private List<object> _errorPassword;
		public List<object> errorPassword
		{
			get => _errorPassword;
			set
			{
				if (value != null)
				{
					_errorPassword = value;
				}
			}
		}

		private List<object> _errorPasswordConfirm;
		public List<object> errorPasswordConfirm
		{
			get => _errorPasswordConfirm;
			set
			{
				if (value != null)
				{
					_errorPasswordConfirm = value;
				}
			}
		}

		async Task OnRegister()
		{
			StringBuilder sb = new StringBuilder();

            if (_isFirstnameValid)
            {
                if (_isLastnameValid)
                {
                    if (_isIDValid)
                    {
                        if (_isEmailValid)
                        {
                            if (_isPasswordValid)
                            {
                                if (_isPasswordConfirmValid)
                                {
                                    Console.WriteLine($"{_firstname} {_lastname}\n{ _id }\n{ _email }\n{ _password }");
									await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
								}
								else
                                {
									sb.Clear();

                                    foreach (var error in _errorPasswordConfirm)
                                    {
                                        if (error is string)
                                        {
											sb.Append(((string)error).ToString() + " ");
                                        }
                                    }

									await Shell.Current.DisplayAlert("Invalid Password", $"The entered password is invalid! { sb.ToString() }Please try again.", "OK");
                                }
                            }
                            else
                            {
								sb.Clear();

								foreach (var error in _errorPassword)
								{
									if (error is string)
									{
										sb.Append(((string)error).ToString() + " ");
									}
								}

								await Shell.Current.DisplayAlert("Invalid Password", $"The entered password is invalid! { sb.ToString() }Please try again.", "OK");
							}
                        }
                        else
                        {
							sb.Clear();

							foreach (var error in _errorEmail)
							{
								if (error is string)
								{
									sb.Append(((string)error).ToString() + " ");
								}
							}

							await Shell.Current.DisplayAlert("Invalid Email", $"The entered email is invalid! { sb.ToString() }Please try again.", "OK");
						}
                    }
                    else
                    {
						sb.Clear();

						foreach (var error in _errorID)
						{
							if (error is string)
							{
								sb.Append(((string)error).ToString() + " ");
							}
						}

						await Shell.Current.DisplayAlert("Invalid Student ID", $"The entered ID is invalid! { sb.ToString() }Please try again.", "OK");
					}
                }
                else
                {
					sb.Clear();

					foreach (var error in _errorLastname)
					{
						if (error is string)
						{
							sb.Append(((string)error).ToString() + " ");
						}
					}

					await Shell.Current.DisplayAlert("Invalid Lastname", $"The entered lastname is invalid! { sb.ToString() }Please try again.", "OK");
				}
            }
            else
            {
				sb.Clear();

				foreach (var error in _errorFirstname)
				{
					if (error is string)
					{
						sb.Append(((string)error).ToString() + " ");
					}
				}

				await Shell.Current.DisplayAlert("Invalid Firstname", $"The entered firstname is invalid! { sb.ToString() }Please try again.", "OK");
			}
		}

		async Task GoToLogin()
        {
			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}
	}
}

