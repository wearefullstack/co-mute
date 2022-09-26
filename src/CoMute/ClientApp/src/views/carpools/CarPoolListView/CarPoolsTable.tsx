import React from 'react';
import type { FC } from 'react';
import { Link as RouterLink } from 'react-router-dom';
import PropTypes from 'prop-types';
import PerfectScrollbar from 'react-perfect-scrollbar';
import { Box, colors, IconButton, SvgIcon, Table, TableBody,
  TableCell, TableHead, TableRow, Tooltip } from '@material-ui/core';
import {
  Edit as EditIcon,
  ArrowRight as ArrowRightIcon,
  PlusCircle as PlusCircleIcon,
  MinusCircle as MinusCircleIcon
} from 'react-feather';
import type { CarPool } from 'src/types/carPool';
import { UserCarPool } from 'src/types/UserCarPool';

interface CarPoolsTableProps {
  paginatedCarPools: CarPool[];
  userId: number;
  userJoinedCarPools: UserCarPool[];
  joinCarPool: (userId: number, carPoolId: number) => Promise<void>;
  leaveCarPool: (userId: number, carPoolId: number) => Promise<void>;
}

const CarPoolsTable: FC<CarPoolsTableProps> = ({
  paginatedCarPools, userId, userJoinedCarPools, joinCarPool, leaveCarPool
}) => (
  <Box>
    <PerfectScrollbar>
      <Box minWidth={700}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>
                Origin
              </TableCell>
              <TableCell>
                Destination
              </TableCell>
              <TableCell>
                Available Seats
              </TableCell>
              <TableCell>
                Departure Time
              </TableCell>
              <TableCell>
                Arrival Time
              </TableCell>
              <TableCell>
                Days Available
              </TableCell>
              <TableCell align="right">
                Actions
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {paginatedCarPools.map((carPool) => (
              <TableRow hover key={carPool.id}>
                <TableCell>{carPool.origin}</TableCell>
                <TableCell>{carPool.destination}</TableCell>
                <TableCell>{carPool.availableSeats}</TableCell>
                <TableCell>{carPool.departureTime}</TableCell>
                <TableCell>{carPool.arrivalTime}</TableCell>
                <TableCell>{carPool.daysAvailable}</TableCell>
                <TableCell align="right">
                    {userId === carPool.ownerId && 
                      <IconButton component={RouterLink}
                        to={`/app/carPools/${carPool.id}/edit`}>
                        <Tooltip title="Edit car pool">
                          <SvgIcon fontSize="small">
                            <EditIcon color={colors.blue[900]}/>
                          </SvgIcon>
                        </Tooltip>
                      </IconButton>}
                    {userId !== carPool.ownerId && !userJoinedCarPools.some(ucp => ucp.carPoolId === carPool.id) &&
                      <IconButton onClick={() => joinCarPool(userId, carPool.id)}>
                        <Tooltip title="Join car pool">
                          <SvgIcon fontSize="small">
                            <PlusCircleIcon color={colors.green[900]} />
                          </SvgIcon>
                        </Tooltip>
                      </IconButton>}
                    {userId !== carPool.ownerId && userJoinedCarPools.some(ucp => ucp.carPoolId === carPool.id) &&
                      <IconButton onClick={() => leaveCarPool(userId, carPool.id)}>
                        <Tooltip title="Leave car pool">
                          <SvgIcon fontSize="small">
                            <MinusCircleIcon color={colors.red[900]}/>
                          </SvgIcon>
                        </Tooltip>
                      </IconButton>}
                    <IconButton component={RouterLink}
                        to={`/app/carPools/${carPool.id}`}>
                        <Tooltip title="View details">
                          <SvgIcon fontSize="small">
                            <ArrowRightIcon color={colors.common.white}/>
                          </SvgIcon>
                        </Tooltip>
                    </IconButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </Box>
    </PerfectScrollbar>
  </Box>
);

CarPoolsTable.propTypes = {
  paginatedCarPools: PropTypes.array.isRequired,
  userId: PropTypes.number.isRequired,
  userJoinedCarPools: PropTypes.array.isRequired,
  joinCarPool: PropTypes.func.isRequired,
  leaveCarPool: PropTypes.func.isRequired
};
export default CarPoolsTable;
