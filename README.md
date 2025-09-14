# 📦 Warehouse Management System – ASP.NET Web API (.NET 9)

## Overview

This is a **Warehouse Management System (WMS)** built using **ASP.NET Web API (.NET 9)**.  
Its aim is to manage all stages of transfers between warehouses — from picking through verification, boxing, lot assignment, to shipment and final approval.

## 🛠 Tech Stack

- **Framework:** .NET 9 / ASP.NET Core Web API  
- **Language:** C#  
- **Database:** SQL Server (or another relational DB)  
- **Architecture:**  
  - WarehouseAPI (Web API project)  
  - WarehouseBLL (Business Logic Layer)  
  - WarehouseDAL (Data Access Layer)  
- **Other Tools:**  
  - Entity Framework Core (for ORM)    
  - Swagger

## 🎯 User Stories & Workflow

Epic 1 – Picking Creation  
- *US 1.1 Create Picking*  
  As a warehouse user, I want to create a new picking so I can start a transfer operation.  
  Acceptance Criteria: select source warehouse, description, date, save via API with unique ID.

Epic 2 – Transfer Selection & Verification  
- *US 2.1 View On‑Hold Transfers*  
- *US 2.2 Verify Items by Scanning*  
- *US 2.3 Confirm Sorted*

Epic 3 – Boxing  
- *US 3.1 Assign Items to Boxes*  
- *US 3.2 Confirm Boxed*

Epic 4 – Lot Assignment  
- *US 4.1 Assign Boxes to Lots*

Epic 5 – Shipment Process  
- *US 5.1 Mark Lots as Ready to Ship*  
- *US 5.2 Mark Lots as In Transit*  
- *US 5.3 Approve Shipment at Destination*

## 🚀 Getting Started

### Prerequisites

- .NET 9 SDK  
- SQL Server instance (or equivalent)  
- IDE: Visual Studio 2022/2023
