import React from 'react';
import type { FC } from 'react';
import {
  Box,
  Container,
  makeStyles,
} from '@material-ui/core';
import type { Theme } from 'src/theme';
import Page from 'src/components/Page';
import CarPoolEditForm from './CarPoolEditForm';
import Header from './Header';

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.dark,
    minHeight: '100%',
    paddingTop: theme.spacing(3),
    paddingBottom: theme.spacing(3),
  },
}));

const CarPoolEditView: FC = () => {
  const classes = useStyles();
  return (
    <Page
      className={classes.root}
      title="Update Car Pool">
      <Container maxWidth={false}>
        <Header />
      </Container>
      <Box mt={3}>
        <Container maxWidth="lg">
          <CarPoolEditForm />
        </Container>
      </Box>
    </Page>
  );
};

export default CarPoolEditView;
