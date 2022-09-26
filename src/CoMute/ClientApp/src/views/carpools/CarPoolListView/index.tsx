import React, { useEffect, useState } from 'react';
import type { FC } from 'react';
import {Box, Container, makeStyles} from '@material-ui/core';
import type { Theme } from 'src/theme';
import Page from 'src/components/Page';
import Results from './Results';
import Header from './Header';
import { User } from 'src/types/user';

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.dark,
    minHeight: '100%',
    paddingTop: theme.spacing(3),
    paddingBottom: theme.spacing(3),
  },
}));

const CarPoolListView: FC = () => {
  const classes = useStyles();
  const [userId, setUserId] = useState<number | null>();
  useEffect(() => {
    const user: User = JSON.parse(localStorage.getItem('user'));
    setUserId(user.id);
  }, [])
  return (
    <Page
      className={classes.root}
      title="Car Pool List">
      <Container maxWidth={false}>
        <Header />
        <Box mt={3}>
          {userId && <Results userId={userId}/>}
        </Box>
      </Container>
    </Page>
  );
};

export default CarPoolListView;
