<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="BITWebApplication.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <img src="Images/server.jpg" alt="servers" class="img-fluid" />
    </section>
    <br />
    <br />
  


  <!-- Marketing messaging and featurettes
  ================================================== -->
  <!-- Wrap the rest of the page in another container to center all the content. -->

  <div class="container marketing">

    <div class="row">
      <div class="col-lg-4">
          <img class="rounded-circle" src="Images/fiber.jpg" height="140" width="140" />
        

        <h2>Connected</h2>
        <p>Upgrade your local network to keep up with today's ever growing buissness demands. </p>
        <p><a class="btn btn-secondary" href="#">View Our Packages &raquo;</a></p>
      </div><!-- /.col-lg-4 -->
      <div class="col-lg-4">
        <img class="rounded-circle" src="Images/tech.jpg" height="140" width="140" />

        <h2>Cutting-Edge</h2>
        <p>Speak with us about how we can revolutionise your workplace tech. Have Questions?</p>
        <p><a class="btn btn-secondary" href="#">Contact Us &raquo;</a></p>
      </div><!-- /.col-lg-4 -->
      <div class="col-lg-4">
        <img class="rounded-circle" src="Images/computer2.jpg" height="140" width="140"/>

        <h2>Convenient</h2>
        <p>Book a technition to solve any tech concerns on the spot.</p>
        <p><a class="btn btn-secondary" href="#">Sign Up Now &raquo;</a></p>
      </div><!-- /.col-lg-4 -->
    </div><!-- /.row -->


    <!-- START THE FEATURETTES -->

    <hr class="featurette-divider">

    <div class="row featurette">
      <div class="col-md-7">
        <h2 class="featurette-heading">Australias Technical Leader <span class="text-muted">For All Solutions</span></h2>
        <p class="lead">Here at BIT we are at the forefront of Australias technological solutions. We were the winner of the 2020 "Most Innovative Solution" award by BIT magazine.</p>
      </div>
      <div class="col-md-5">
        <img src="Images/cyberspace.jpg" alt="Networking" width="450"/>

      </div>
    </div>

    <hr class="featurette-divider">

    <div class="row featurette">
      <div class="col-md-7 order-md-2">
        <h2 class="featurette-heading">Over 100 Technitions <span class="text-muted">Australia Wide</span></h2>
        <p class="lead">Our technitions are trained in everything from databases to your workspaces. They are friendly and highly trained for any situation. </p>
      </div>
      <div class="col-md-5 order-md-1">
        <img src="Images/pc.jpg" alt="computer repair" width="450"/>

      </div>
    </div>

    <hr class="featurette-divider">

    <div class="row featurette">
      <div class="col-md-7">
        <h2 class="featurette-heading">We Come To You  <span class="text-muted">Book an Appointment Now.</span></h2>
        <p class="lead">Our technitions are booked out on a proximity basis. We will find someone local to assist where needed.</p>
      </div>
      <div class="col-md-5">
       <img src="Images/computer.jpg" alt="Computer" width="450"/>

      </div>
    </div>


    <!-- /END THE FEATURETTES -->

  </div><!-- /.container -->
</asp:Content>
