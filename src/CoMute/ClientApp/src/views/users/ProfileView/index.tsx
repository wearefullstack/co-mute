import React, { useState } from 'react';
import type { FC } from 'react';
import {
  Box,
  Container,
  makeStyles,
} from '@material-ui/core';
import type { Theme } from 'src/theme';
import Page from 'src/components/Page';
import EditForm from './EditForm';
import Header from './Header';

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.dark,
    minHeight: '100%',
    paddingTop: theme.spacing(3),
    paddingBottom: theme.spacing(3),
  },
}));

const ProfileView: FC = () => {
  const classes = useStyles();
  const [editMode, setEditMode] = useState<boolean>(false);

  return (
    <Page className={classes.root} title="Edit User">
      <Container maxWidth={false}>
        <Header setEditMode={setEditMode} editMode={editMode}/>
      </Container>
      <Box mt={3}>
        <Container maxWidth="lg">
          <EditForm setEditMode={setEditMode} editMode={editMode}/>
        </Container>
      </Box>
    </Page>
  );
};

export default ProfileView;
