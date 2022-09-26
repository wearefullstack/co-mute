import React, { FC, useEffect, useState } from 'react';
import { Link as RouterLink } from 'react-router-dom';
import PropTypes from 'prop-types';
import clsx from 'clsx';
import {
  Breadcrumbs,
  Link,
  Typography,
  makeStyles,
  Button,
  Grid,
  SvgIcon,
} from '@material-ui/core';
import { Edit as EditIcon } from 'react-feather';
import NavigateNextIcon from '@material-ui/icons/NavigateNext';
import { User } from 'src/types/user';

interface HeaderProps {
  className?: string;
  setEditMode: (mode: boolean) => void;
  editMode: boolean;
}

const useStyles = makeStyles(() => ({
  root: {},
}));

const Header: FC<HeaderProps> = ({ className, setEditMode, editMode, ...rest }) => {
  const classes = useStyles();
  const [user, setUser] = useState<User | null>(null);

  useEffect(() =>{
    const _user: User = JSON.parse(localStorage.getItem("user"));
    setUser(_user);
  },[])

  return (
    <Grid
      className={clsx(classes.root, className)}
      container
      justify="space-between"
      spacing={3}
      {...rest}>
      <Grid item>
        <Breadcrumbs
          separator={<NavigateNextIcon fontSize="small" />}
          aria-label="breadcrumb">
          <Link variant="body1" color="inherit" to="/app" component={RouterLink}>
            Home
          </Link>
          <Typography variant="body1" color="textPrimary">
            Profile
          </Typography>
        </Breadcrumbs>
        <Typography variant="h3" color="textPrimary">
          {user && user.name} {user && user.surname}
        </Typography>
      </Grid>
      <Grid item>
      {!editMode && <Button
          color="secondary"
          variant="contained"
          onClick={() => setEditMode(true)}
          startIcon={(
            <SvgIcon fontSize="small">
              <EditIcon />
            </SvgIcon>
          )}>
          Update
        </Button>}
      </Grid>
    </Grid>
  );
};

Header.propTypes = {
  className: PropTypes.string,
};

export default Header;
