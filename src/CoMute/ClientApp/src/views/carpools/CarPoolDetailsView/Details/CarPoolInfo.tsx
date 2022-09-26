import React, { useCallback, useEffect, useState } from 'react';
import type { FC } from 'react';
import PropTypes from 'prop-types';
import clsx from 'clsx';
import {
  Card,
  CardHeader,
  Divider,
  Table,
  TableBody,
  TableCell,
  TableRow,
  Typography,
  makeStyles,
} from '@material-ui/core';
import type { Theme } from 'src/theme';
import type { CarPool } from 'src/types/carPool';
import { User } from 'src/types/user';
import axios from 'axios';

interface CarPoolInfoProps {
  carPool: CarPool;
  className?: string;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  fontWeightMedium: {
    fontWeight: theme.typography.fontWeightMedium,
  },
}));

const CarPoolInfo: FC<CarPoolInfoProps> = ({
  carPool,
  className,
  ...rest
}) => {
  const classes = useStyles();
  const [owner, setOwner] = useState<User | null>();
  console.log(carPool);
  const getOwner = useCallback(async () => {
    try {
      const response = await axios.get(`/users/${carPool.ownerId}`);
      setOwner(response.data);
    } catch (err) {
      console.error(err);
    }
  }, [carPool]);

  useEffect(() => {
    getOwner();
  }, [getOwner]);

  return (
    <Card
      className={clsx(classes.root, className)}
      {...rest}>
      <CardHeader title="Car Pool Details" />
      <Divider />
      <Table>
        <TableBody>
          {owner && <TableRow>
            <TableCell className={classes.fontWeightMedium}>
              Owner
            </TableCell>
            <TableCell>
              <Typography variant="body2"color="textSecondary">
                {owner.name} {owner.surname}
              </Typography>
            </TableCell>
          </TableRow>}
          <TableRow>
            <TableCell className={classes.fontWeightMedium}>
              Departure time
            </TableCell>
            <TableCell>
              <Typography variant="body2"color="textSecondary">
                {carPool.departureTime}
              </Typography>
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell className={classes.fontWeightMedium}>
              Arrival time
            </TableCell>
            <TableCell>
              <Typography variant="body2"color="textSecondary">
                {carPool.arrivalTime}
              </Typography>
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell className={classes.fontWeightMedium}>
              Origin
            </TableCell>
            <TableCell>
              <Typography variant="body2"color="textSecondary">
                {carPool.origin}
              </Typography>
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell className={classes.fontWeightMedium}>
              Destination
            </TableCell>
            <TableCell>
              <Typography variant="body2"color="textSecondary">
                {carPool.destination}
              </Typography>
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell className={classes.fontWeightMedium}>
              Available seats
            </TableCell>
            <TableCell>
              <Typography variant="body2"color="textSecondary">
                {carPool.availableSeats}
              </Typography>
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell className={classes.fontWeightMedium}>
              Days available
            </TableCell>
            <TableCell>
              <Typography variant="body2"color="textSecondary">
                {carPool.daysAvailable}
              </Typography>
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell className={classes.fontWeightMedium}>
              Notes
            </TableCell>
            <TableCell>
              <Typography variant="body2"color="textSecondary">
                {carPool.notes}
              </Typography>
            </TableCell>
          </TableRow>
        </TableBody>
      </Table>
    </Card>
  );
};

CarPoolInfo.propTypes = {
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

export default CarPoolInfo;
