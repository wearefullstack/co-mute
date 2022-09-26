import React, {
  createContext,
  useEffect,
  useReducer
} from 'react';
import type { FC, ReactNode } from 'react';
import jwtDecode from 'jwt-decode';
import type { User } from 'src/types/user';
import SplashScreen from 'src/components/SplashScreen';
import axios from 'axios';
import environment from 'src/utils/environments';
import { useHistory } from 'react-router-dom';

interface AuthState {
  isInitialised: boolean;
  isAuthenticated: boolean;
  user: User | null;
}

interface AuthContextValue extends AuthState {
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
  register: (name: string, surname: string, email: string, phone: string, password: string) => Promise<void>;
}

interface AuthProviderProps {
  children: ReactNode;
}

type InitialiseAction = {
  type: 'INITIALISE';
  payload: {
    isAuthenticated: boolean;
    user: User | null;
  };
};

type LoginAction = {
  type: 'LOGIN';
  payload: {
    user: User;
  };
};

type LogoutAction = {
  type: 'LOGOUT';
};

type RegisterAction = {
  type: 'REGISTER';
  payload: {
    user: User;
  };
};

type Action =
  | InitialiseAction
  | LoginAction
  | LogoutAction
  | RegisterAction;

const initialAuthState: AuthState = {
  isAuthenticated: false,
  isInitialised: false,
  user: null
};

const isValidToken = (token: string): boolean => {
  if (!token) {
    return false;
  }

  const decoded: any = jwtDecode(token);
  const currentTime = Date.now() / 1000;

  return decoded.exp > currentTime;
};

const setSession = (user: User | null): void => {
  if (user?.token) {
    localStorage.setItem('token', user.token);
    localStorage.setItem('user', JSON.stringify(user));
    axios.defaults.baseURL = environment.apiUrl;
    axios.defaults.headers.Authorization = `Bearer ${user.token}`;
    axios.defaults.headers['Content-Type'] = 'application/json';
  } else {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    delete axios.defaults.headers.Authorization;
  }
};

const reducer = (state: AuthState, action: Action): AuthState => {
  switch (action.type) {
    case 'INITIALISE': {
      const { isAuthenticated, user } = action.payload;

      return {
        ...state,
        isAuthenticated,
        isInitialised: true,
        user
      };
    }
    case 'LOGIN': {
      const { user } = action.payload;

      return {
        ...state,
        isAuthenticated: true,
        user
      };
    }
    case 'LOGOUT': {
      return {
        ...state,
        isAuthenticated: false,
        user: null
      };
    }
    case 'REGISTER': {
      const { user } = action.payload;

      return {
        ...state,
        isAuthenticated: false,
        user
      };
    }
    default: {
      return { ...state };
    }
  }
};

const AuthContext = createContext<AuthContextValue>({
  ...initialAuthState,
  login: () => Promise.resolve(),
  logout: () => { },
  register: () => Promise.resolve()
});

export const AuthProvider: FC<AuthProviderProps> = ({ children }) => {
  const [state, dispatch] = useReducer(reducer, initialAuthState);
  const history = useHistory();
  axios.defaults.baseURL = environment.apiUrl;
  axios.defaults.headers['Content-Type'] = 'application/json';

  const login = async (email: string, password: string) => {
    const endpoint = '/auth/authenticate';
    const data = JSON.stringify({ email, password });
    const response = await axios.post(endpoint, data);
    const user: User = response.data;
    setSession(user);
    dispatch({
      type: 'LOGIN',
      payload: {
        user
      }
    });
  };

  const logout = () => {
    setSession(null);
    dispatch({ type: 'LOGOUT' });
  };

  const register = async (name: string, surname: string, email: string, phone: string, password: string) => {
    const endpoint = '/users/register';
    const data = JSON.stringify({ name, surname, email, phone, password });
    const response = await axios.post(endpoint, data);
    const user: User = response.data;
    history.push('/login');
    dispatch({
      type: 'REGISTER',
      payload: {
        user
      }
    });
  };

  useEffect(() => {
    const initialise = async () => {
      try {
        const token: string | null = window.localStorage.getItem('token');
        const localUser: User = JSON.parse(window.localStorage.getItem('user'));

        if (token && isValidToken(token)) {
          setSession(localUser);
          const endpoint =`/users/${localUser.id}`;
          const response = await axios.get(endpoint);
          const user = response.data;

          dispatch({
            type: 'INITIALISE',
            payload: {
              isAuthenticated: true,
              user
            }
          });
        } else {
          localStorage.removeItem('user');
          dispatch({
            type: 'INITIALISE',
            payload: {
              isAuthenticated: false,
              user: null
            }
          });
        }
      } catch (err) {
        console.error(err);
        dispatch({
          type: 'INITIALISE',
          payload: {
            isAuthenticated: false,
            user: null
          }
        });
      }
    };

    initialise();
  }, []);

  if (!state.isInitialised) {
    return <SplashScreen />;
  }

  return (
    <AuthContext.Provider
      value={{
        ...state,
        login,
        logout,
        register
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;