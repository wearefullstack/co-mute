import React, {
  Suspense,
  Fragment,
  lazy,
} from 'react';
import {
  Switch,
  Redirect,
  Route,
} from 'react-router-dom';
import DashboardLayout from 'src/layouts/DashboardLayout';
import MainLayout from 'src/layouts/MainLayout';
import LoadingScreen from 'src/components/LoadingScreen';
import AuthGuard from 'src/components/AuthGuard';
import GuestGuard from 'src/components/GuestGuard';

type Routes = {
  id?: number;
  exact?: boolean;
  path?: string | string[];
  guard?: any;
  layout?: any;
  component?: any;
  routes?: Routes;
}[];

export const renderRoutes = (routes: Routes = []): JSX.Element => (
  <Suspense fallback={<LoadingScreen />}>
    <Switch>
      {routes.map((route) => {
        const Guard = route.guard || Fragment;
        const Layout = route.layout || Fragment;
        const Component = route.component;

        return (
          <Route
            key={route.id}
            path={route.path}
            exact={route.exact}
            render={(props) => (
              <Guard>
                <Layout>
                  {route.routes
                    ? renderRoutes(route.routes)
                    : <Component {...props} />}
                </Layout>
              </Guard>
            )}
          />
        );
      })}
    </Switch>
  </Suspense>
);

const routes: Routes = [
  {
    id: 1,
    exact: true,
    path: '/404',
    component: lazy(() => import('src/views/errors/NotFoundView'))
  },
  {
    id: 2,
    exact: true,
    guard: GuestGuard,
    path: '/login',
    component: lazy(() => import('src/views/auth/LoginView'))
  },
  {
    id: 3,
    exact: true,
    guard: GuestGuard,
    path: '/register',
    component: lazy(() => import('src/views/auth/RegisterView'))
  },
  {
    id: 4,
    path: '/app',
    guard: AuthGuard,
    layout: DashboardLayout,
    routes: [
      {
        id: 7,
        exact: true,
        path: '/app/carpools',
        component: lazy(() => import('src/views/carpools/CarPoolListView'))
      },
      {
        id: 8,
        exact: true,
        path: '/app/carpools/add',
        component: lazy(() => import('src/views/carpools/CarPoolAddView'))
      },
      {
        id: 9,
        exact: true,
        path: '/app/carpools/:carPoolId',
        component: lazy(() => import('src/views/carpools/CarPoolDetailsView'))
      },
      {
        id: 10,
        exact: true,
        path: '/app/carpools/:carPoolId/edit',
        component: lazy(() => import('src/views/carpools/CarPoolEditView'))
      },
      {
        id: 11,
        exact: true,
        path: '/app',
        component: () => <Redirect to="/app/carpools" />
      },
      {
        id: 12,
        exact: true,
        path: '/app/profile',
        component: lazy(() => import('src/views/users/ProfileView'))
      },
      {
        id: 12,
        component: () => <Redirect to="/404" />,
      },
    ],
  },
  {
    id: 13,
    path: '*',
    guard: AuthGuard,
    layout: MainLayout,
    routes: [
      {
        id: 14,
        exact: true,
        path: '/',
        component: () => <Redirect to="/app" />,
      },
      {
        id: 15,
        component: () => <Redirect to="/404" />,
      },
    ],
  },
];

export default routes;
