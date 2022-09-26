import React, { FC } from 'react';
import PropTypes from 'prop-types';
import clsx from 'clsx';
import { Grid, makeStyles } from '@material-ui/core';
import CarPoolInfo from './CarPoolInfo';
import type { CarPool } from 'src/types/carPool';

interface DetailsProps {
  carPool: CarPool;
  className?: string;
}

const useStyles = makeStyles(() => ({
  root: {},
}));

const Details: FC<DetailsProps> = ({
  carPool,
  className,
  ...rest
}) => {
  const classes = useStyles();

  return (
    <Grid className={clsx(classes.root, className)}
      container spacing={3} {...rest}>
      <Grid item xs={12}>
        <CarPoolInfo carPool={carPool} />
      </Grid>
    </Grid>
  );
};

Details.propTypes = {
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
  })
};

export default Details;
