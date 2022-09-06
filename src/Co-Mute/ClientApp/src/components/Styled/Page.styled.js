import styled from "styled-components";
import Page from "../Shared/Page";
import PageHeader from "../Shared/PageHeader";


export const PageContainer = styled(Page)`
    display: flex;
    overflow-y: auto;
`

export const StyledPageHeader = styled(PageHeader)`
    font-size: 1.5em;
    color: #349e57;
    font-family: sans-serif;
    font-weight: 400;
`