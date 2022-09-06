import React from 'react'

function Page({children, className}) {
  return (
    <section className={className}>{children}</section>
  )
}

export default Page