import React, { useCallback, useEffect, useState } from 'react';
import type { FC, ChangeEvent } from 'react';
import clsx from 'clsx';
import PropTypes from 'prop-types';
import { Box, Card, Divider, InputAdornment, SvgIcon,
  Tab, TablePagination, Tabs, TextField, makeStyles, colors
} from '@material-ui/core';
import {
  Search as SearchIcon,
} from 'react-feather';
import type { Theme } from 'src/theme';
import type { CarPool } from 'src/types/carPool';
import CarPoolsTable from './CarPoolsTable';
import axios from 'axios';
import useIsMountedRef from 'src/hooks/useIsMountedRef';
import { UserCarPool } from 'src/types/UserCarPool';
import { useSnackbar } from 'notistack';

interface ResultsProps {
  className?: string;
  userId: number;
}

type Sort =
  | 'origin|asc'
  | 'destination|asc'
  | 'availableSeats|desc'
  | 'daysAvailable|desc';

interface SortOption {
  value: Sort,
  label: string
}

enum CarPoolTabs {
  Joined,
  Mine,
  Available,
  All
}

const tabs = [
  {
    value: CarPoolTabs.Joined,
    label: 'Joined Car Pools',
  },{
    value: CarPoolTabs.Mine,
    label: 'My Car Pools',
  },
  {
    value: CarPoolTabs.Available,
    label: 'Available Car Pools',
  },
  {
    value: CarPoolTabs.All,
    label: 'All Car Pools',
  }
];

const sortOptions: SortOption[] = [
  {
    value: 'origin|asc',
    label: 'Origin',
  },
  {
    value: 'destination|asc',
    label: 'Destination',
  },
  {
    value: 'availableSeats|desc',
    label: 'Available seats',
  },
  {
    value: 'daysAvailable|desc',
    label: 'Days available',
  }
];

const applyQuery = (carPools: CarPool[], query: string): CarPool[] => carPools?.filter((carPool) => {
  let matches = true;

  if (query) {
    const properties = ['origin','destination','daysAvailable'];
    let containsQuery = false;

    properties.forEach((property) => {
      if (carPool[property]?.toLowerCase().includes(query.toLowerCase())) {
        containsQuery = true;
      }
    });

    if (!containsQuery) {
      matches = false;
    }
  }

  return matches;
});

const applyPagination = (carPools: CarPool[], page: number,
  limit: number): CarPool[] => carPools.slice(page * limit, page * limit + limit);

const descendingComparator = (a: CarPool, b: CarPool, orderBy: string): number => {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }

  if (b[orderBy] > a[orderBy]) {
    return 1;
  }

  return 0;
};

const getComparator = (order: 'asc' | 'desc', orderBy: string) => (order === 'desc'
  ? (a: CarPool, b: CarPool) => descendingComparator(a, b, orderBy)
  : (a: CarPool, b: CarPool) => -descendingComparator(a, b, orderBy));

const applySort = (customers: CarPool[], sort: Sort): CarPool[] => {
  const [orderBy, order] = sort.split('|') as [string, 'asc' | 'desc'];
  const comparator = getComparator(order, orderBy);
  const stabilizedThis = customers.map((el, index) => [el, index]);

  stabilizedThis.sort((a, b) => {
    // @ts-ignore
    const tmpOrder = comparator(a[0], b[0]);

    if (tmpOrder !== 0) return tmpOrder;

    // @ts-ignore
    return a[1] - b[1];
  });

  // @ts-ignore
  return stabilizedThis.map((el) => el[0]);
};

const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  queryField: {
    width: 500,
  },
  bulkOperations: {
    position: 'relative',
  },
  bulkActions: {
    paddingLeft: 4,
    paddingRight: 4,
    marginTop: 6,
    position: 'absolute',
    width: '100%',
    zIndex: 2,
    backgroundColor: theme.palette.background.default,
  },
  bulkAction: {
    marginLeft: theme.spacing(2),
  },
  avatar: {
    height: 42,
    width: 42,
    marginRight: theme.spacing(1),
  },
  joinButton:{
    backgroundColor: colors.green[700],
    color: colors.common.white,
    padding: '3px 21px',
    borderRadius: 5,
    textTransform: 'inherit'
  },
  leaveButton: {
    backgroundColor: colors.red[700],
    color: colors.common.white,
    padding: '3px 16px',
    borderRadius: 5,
    textTransform: 'inherit'
  }
}));

const Results: FC<ResultsProps> = ({
  className,
  userId,
  ...rest
}) => {
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();
  const [currentTab, setCurrentTab] = useState<CarPoolTabs>(CarPoolTabs.Joined);
  const [page, setPage] = useState<number>(0);
  const [limit, setLimit] = useState<number>(5);
  const [query, setQuery] = useState<string>('');
  const [sort, setSort] = useState<Sort>(sortOptions[0].value);
  const [endpoint, setEndpoint] = useState<string>(`/carpools/${userId}/${CarPoolTabs.Joined}`)
  const isMountedRef = useIsMountedRef();
  const [carPools, setCarPools] = useState<CarPool[]>([]);
  const [userJoinedCarPools, setUserJoinedCarPools] = useState<UserCarPool[]>([]);

  const getCarPools = useCallback(async () => {
    try {
      const response = await axios.get(endpoint);

      if (isMountedRef.current) {
        setCarPools(response.data);
      }
    } catch (err) {
      console.error(err);
    }
  }, [endpoint, isMountedRef]);

  const getUserJoinedCarPools = useCallback(async () => {
    try {
      const userJoinedCarPoolsEndpoint = `/userCarPools/${userId}`;
      const response = await axios.get(userJoinedCarPoolsEndpoint);

      if (isMountedRef.current) {
        setUserJoinedCarPools(response.data);
      }
    } catch (err) {
      console.error(err);
    }
  }, [userId, isMountedRef]);

  useEffect(() => {
    getCarPools();
    getUserJoinedCarPools();
  }, [getCarPools, getUserJoinedCarPools]);
  
  const joinCarPool = useCallback(async (userId: number, carPoolId: number) => {
    try {
      const data = JSON.stringify({
        carPoolId: carPoolId,
        userId: userId
      })
      const joinCarPoolEndpoint = `/userCarPools/join`;
      const response = await axios.post(joinCarPoolEndpoint, data);
      
      if (isMountedRef.current) {
        getCarPools();
        setUserJoinedCarPools(response.data);
        enqueueSnackbar('You have successfully joined this car pool.', { variant: 'success' });
      }
    } catch (err) {
      console.error(err);
      enqueueSnackbar(err, { variant: 'error' });
    }
  }, [isMountedRef, enqueueSnackbar, getCarPools]);

  const leaveCarPool = useCallback(async (userId: number, carPoolId: number) => {
    try {
      const data = JSON.stringify({
        carPoolId: carPoolId,
        userId: userId
      })
      const leaveCarPoolEndpoint = `/userCarPools/leave`;
      const response = await axios.post(leaveCarPoolEndpoint, data);
      
      if (isMountedRef.current) {
        getCarPools();
        setUserJoinedCarPools(response.data);
        enqueueSnackbar('You have successfully left this car pool.', { variant: 'success' });
      }
    } catch (err) {
      console.error(err);
      enqueueSnackbar(err, { variant: 'error' });
    }
  }, [isMountedRef, enqueueSnackbar, getCarPools]);
  
  const handleTabsChange = (value: CarPoolTabs): void => {
    setCurrentTab(value);
    setEndpoint(`/carpools/${userId}/${value}`);
  };

  const handleQueryChange = (event: ChangeEvent<HTMLInputElement>): void => {
    event.persist();
    setQuery(event.target.value);
  };

  const handleSortChange = (event: ChangeEvent<HTMLInputElement>): void => {
    event.persist();
    setSort(event.target.value as Sort);
  };

  const handlePageChange = (newPage: number): void => {
    setPage(newPage);
  };

  const handleLimitChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setLimit(parseInt(event.target.value, 16));
  };

  const filteredCarPools = applyQuery(carPools, query);
  const sortedCarPools = applySort(filteredCarPools, sort);
  const paginatedCarPools = applyPagination(sortedCarPools, page, limit);

  return (
    <Card
      className={clsx(classes.root, className)}
      {...rest}>
      <Tabs
        onChange={(event, value) => handleTabsChange(value)}
        scrollButtons="auto"
        textColor="secondary"
        value={currentTab}
        variant="scrollable">
        {tabs.map((tab) => (
          <Tab
            key={tab.value}
            value={tab.value}
            label={tab.label}
          />
        ))}
      </Tabs>
      <Divider />
      <Box
        p={2}
        minHeight={56}
        display="flex"
        alignItems="center">
        <TextField
          className={classes.queryField}
          InputProps={{
            startAdornment: (
              <InputAdornment position="start">
                <SvgIcon fontSize="small" color="action">
                  <SearchIcon />
                </SvgIcon>
              </InputAdornment>
            ),
          }}
          onChange={handleQueryChange}
          placeholder="Search car pools"
          value={query}
          variant="outlined"/>
        <Box flexGrow={1} />
        <TextField
          label="Sort By"
          name="sort"
          onChange={handleSortChange}
          select
          SelectProps={{ native: true }}
          value={sort}
          variant="outlined">
          {sortOptions.map((option) => (
            <option
              key={option.value}
              value={option.value}>
              {option.label}
            </option>
          ))}
        </TextField>
      </Box>
      <CarPoolsTable 
        userId={userId} 
        paginatedCarPools={paginatedCarPools} 
        userJoinedCarPools={userJoinedCarPools}
        joinCarPool={joinCarPool}
        leaveCarPool={leaveCarPool}/>
      <TablePagination
        component="div"
        count={filteredCarPools.length}
        onChangePage={(event, newPage) => handlePageChange(newPage)}
        onChangeRowsPerPage={handleLimitChange}
        page={page}
        rowsPerPage={limit}
        rowsPerPageOptions={[5, 10, 25]}
      />
    </Card>
  );
};

Results.propTypes = {
  className: PropTypes.string,
  userId: PropTypes.number.isRequired
};

export default Results;
