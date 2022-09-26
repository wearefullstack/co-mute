import React, { useEffect, useState } from 'react';
import type { FC } from 'react';
import { Link as RouterLink } from 'react-router-dom';
import PropTypes from 'prop-types';
import clsx from 'clsx';
import {
  Breadcrumbs,
  Button,
  Grid,
  Link,
  SvgIcon,
  Typography,
  makeStyles,
} from '@material-ui/core';
import NavigateNextIcon from '@material-ui/icons/NavigateNext';
import { Edit as EditIcon } from 'react-feather';
import type { CarPool } from 'src/types/carPool';
import { User } from 'src/types/user';

interface HeaderProps {
  className?: string;
  carPool: CarPool;
}

const useStyles = makeStyles(() => ({
  root: {},
}));

const Header: FC<HeaderProps> = ({ className, carPool, ...rest }) => {
  const classes = useStyles();
  const [userId, setUserId] = useState<number | null>(null);
  
  useEffect(() =>{
    const user:User = JSON.parse(localStorage.getItem('user'));
    if(user){
      setUserId(user.id);
    }
  },[setUserId]);

  return (
    <Grid
      container
      spacing={3}
      justify="space-between"
      className={clsx(classes.root, className)}
      {...rest}>
      <Grid item>
        <Breadcrumbs aria-label="breadcrumb" 
          separator={<NavigateNextIcon fontSize="small" />}>
          <Link variant="body1" color="inherit" to="/app" component={RouterLink}>
            Home
          </Link>
          <Typography variant="body1" color="textPrimary">
            Car Pool
          </Typography>
        </Breadcrumbs>
        <Typography variant="h3" color="textPrimary">
          {`From ${carPool.origin} to ${carPool.destination}`}
        </Typography>
      </Grid>
      <Grid item>
        {userId && userId === carPool.ownerId &&
          <Button
            color="secondary"
            variant="contained"
            component={RouterLink}
            to={`/app/carPools/${carPool.id}/edit`}
            startIcon={(
              <SvgIcon fontSize="small">
                <EditIcon />
              </SvgIcon>
            )}>
            Edit
          </Button>}
      </Grid>
    </Grid>
  );
};

Header.propTypes = {
  className: PropTypes.string,
  carPool: PropTypes.exact({
    id: PropTypes.number.isRequired,
    departureTime: PropTypes.string.isRequired,
    arrivalTime: PropTypes.string.isRequired,
    origin: PropTypes.string.isRequired,
    daysAvailable: PropTypes.any,
    ownerId: PropTypes.number,
    destination: PropTypes.string.isRequired,
    availableSeats: PropTypes.number.isRequired,
    notes: PropTypes.string,
    created: PropTypes.string
  }).isRequired,
};

export default Header;
