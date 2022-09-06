import React from 'react'

function PageHeader({className, text}) {
  return (
    <h1 className={className}>{text}</h1>
  )
}

export default PageHeader