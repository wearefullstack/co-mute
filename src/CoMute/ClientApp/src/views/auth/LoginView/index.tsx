import React from 'react';
import type { FC } from 'react';
import { Link as RouterLink } from 'react-router-dom';
import {
  Box,
  Card,
  CardContent,
  Container,
  Divider,
  Link,
  Typography,
  makeStyles
} from '@material-ui/core';
import type { Theme } from 'src/theme';
import Page from 'src/components/Page';
import Logo from 'src/components/Logo';
import Login from './Login';

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.dark,
    display: 'flex',
    flexDirection: 'column',
    minHeight: '100vh'
  },
  banner: {
    backgroundColor: theme.palette.background.paper,
    paddingBottom: theme.spacing(2),
    paddingTop: theme.spacing(2),
    borderBottom: `1px solid ${theme.palette.divider}`
  },
  bannerChip: {
    marginRight: theme.spacing(2)
  },
  methodIcon: {
    height: 30,
    marginLeft: theme.spacing(2),
    marginRight: theme.spacing(2)
  },
  cardContainer: {
    paddingBottom: 80,
    paddingTop: 80,
  },
  cardContent: {
    padding: theme.spacing(4),
    display: 'flex',
    flexDirection: 'column',
    minHeight: 400
  },
  logoContainer: {
    width: 120,
    margin: '0 auto 40px'
  },
  removeDecorations:{
    textDecorations: 'none'
  },
  title:{
    fontFamily: '"latolight", "Helvetica Neue", Helvetica, Arial, sans-serif',
    fontSize: 30,
    textAlign: 'center'
  }
}));

const LoginView: FC = () => {
  const classes = useStyles();

  return (
    <Page
      className={classes.root}
      title="Login"
    >
     <Container
        className={classes.cardContainer}
        maxWidth="sm"
      >
        <Box
          display="flex"
          justifyContent="center"
          className={classes.logoContainer}>
          <RouterLink to="/" className={classes.removeDecorations}>
            <Logo />
            <Typography className={classes.title}>
              <span style={{color: '#aaaaaa'}}>co</span>-<span style={{color:'#f2622a'}}>mute</span>
            </Typography>
          </RouterLink>
        </Box>
        <Card>
          <CardContent className={classes.cardContent}>
            <Box
              alignItems="center"
              display="flex"
              justifyContent="space-between"
              mb={3}
            >
              <div>
                <Typography
                  color="textPrimary"
                  gutterBottom
                  variant="h2"
                >
                  Sign in
                </Typography>
                <Typography
                  variant="body2"
                  color="textSecondary"
                >
                  Welcome to our platform
                </Typography>
              </div>
            </Box>
            <Box
              flexGrow={1}
              mt={3}
            ><Login />
            </Box>
            <Box my={3}>
              <Divider />
            </Box>
            <Link
              component={RouterLink}
              to="/register"
              variant="body2"
              color="textSecondary"
            >
              Register
            </Link>
          </CardContent>
        </Card>
      </Container>
    </Page>
  );
};

export default LoginView;
