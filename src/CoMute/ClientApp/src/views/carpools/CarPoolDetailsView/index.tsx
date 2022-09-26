import React, {
  useCallback,
  useState,
  useEffect
} from 'react';
import type { FC } from 'react';
import { useParams } from 'react-router-dom';
import {
  Box,
  Container,
  Divider,
  makeStyles,
} from '@material-ui/core';
import axios from 'axios';
import type { Theme } from 'src/theme';
import Page from 'src/components/Page';
import type { CarPool } from 'src/types/carPool';
import Header from './Header';
import Details from './Details';

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.dark,
    minHeight: '100%',
    paddingTop: theme.spacing(3),
    paddingBottom: theme.spacing(3),
  },
}));

const CarPoolDetailsView: FC = () => {
  const classes = useStyles();
  const { carPoolId }: { carPoolId: string } = useParams();
  const [carPool, setCarPool] = useState<CarPool | null>(null);

  const getCarPool = useCallback(async () => {
    try {
      const response = await axios.get(`/carpools/${carPoolId}`);
      setCarPool(response.data);
    } catch (err) {
      console.error(err);
    }
  }, [carPoolId]);

  useEffect(() => {
    getCarPool();
  }, [getCarPool]);

  return (
    <Page
      className={classes.root}
      title="Car Pool Details">
      <Container maxWidth={false}>
        {carPool && <Header carPool={carPool} />}
        <Divider />
        <Box mt={3}>
          {carPool && <Details carPool={carPool} />}
        </Box>
      </Container>
    </Page>
  );
};

export default CarPoolDetailsView;
